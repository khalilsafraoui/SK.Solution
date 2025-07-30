using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Features.Quotes.Queries;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Orders.Queries
{
    public sealed record GetOrderByIdQuery(Guid Id) : IRequest<(bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)>;

    public sealed class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, (bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOrderByIdHandler> _logger;
        public GetOrderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetOrderByIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.Id);
                if (order == null)
                {
                    return (false, null, "Order not found.");
                }
                // Map the order entity to OrderDto
                return (true, _mapper.Map<OrderDto>(order), string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving the order: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while retrieving the order. Please try again later.");
            }
        }
    }
}
