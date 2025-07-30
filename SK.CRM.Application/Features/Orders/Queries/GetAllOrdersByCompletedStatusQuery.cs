using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Enums;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Orders.Queries
{
    

    public sealed record GetAllOrdersByCompletedStatusQuery(int pageIndex = 0, int pageSize = 0, string? userId = null) : IRequest<(bool IsSuccess, List<OrderDto>? OrdersDto, int TotalCount, string ErrorMessage)>;

    public class GetAllOrdersByCompletedStatusQueryHandler : IRequestHandler<GetAllOrdersByCompletedStatusQuery, (bool IsSuccess, List<OrderDto>? OrdersDto, int TotalCount, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllOrdersByCompletedStatusQueryHandler> _logger;
        public GetAllOrdersByCompletedStatusQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllOrdersByCompletedStatusQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<OrderDto>? OrdersDto, int TotalCount, string ErrorMessage)> Handle(GetAllOrdersByCompletedStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string[] statusList = new[] { OrderStatus.StatusCancelled, OrderStatus.StatusCompleted };
                var result = await _unitOfWork.OrderRepository.GetByStatusesAsync(statusList, request.pageIndex, request.pageSize, request.userId);
                if (!result.Orders.Any())
                {
                    return (false, new List<OrderDto>(), 0, "No orders found");
                }
                return (true, _mapper.Map<List<OrderDto>>(result.Orders), result.TotalCount, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving orders: {Message}", ex.Message);
                return (false, null, 0, "An unexpected error occurred while retrieving orders. Please try again later.");

            }
        }
    }
}
