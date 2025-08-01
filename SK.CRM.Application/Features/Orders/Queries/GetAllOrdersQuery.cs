using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Features.Quotes.Queries;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Orders.Queries
{
    public sealed record GetAllOrdersQuery(string? userId = null) : IRequest<(bool IsSuccess, List<OrderDto> ? OrdersDto, string ErrorMessage)>;

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, (bool IsSuccess, List<OrderDto>? OrdersDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllOrdersQueryHandler> _logger;
        public GetAllOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllOrdersQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<OrderDto>? OrdersDto, string ErrorMessage)> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync(request.userId);
                if (!orders.Any())
                {
                    return (false, new List<OrderDto>(), "No order found for the specified user.");
                }
                return (true, _mapper.Map<List<OrderDto>>(orders), string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving orders: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while retrieving orders. Please try again later.");

            }
        }
    }
}
