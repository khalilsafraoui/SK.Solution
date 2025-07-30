using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Features.Items.Dtos;

namespace SK.CRM.Application.Features.Items.Queries
{
    public sealed record ConvertExtensionItemsDtoToOrderDetailsDtoQuery(List<ExtensionItemDto> entities) : IRequest<(bool IsSuccess, List<OrderDetailDto>? Items, string ErrorMessage)>;

    public class ConvertExtensionItemsDtoToOrderDetailsDtoQueryHandler : IRequestHandler<ConvertExtensionItemsDtoToOrderDetailsDtoQuery, (bool IsSuccess, List<OrderDetailDto>? Items, string ErrorMessage)>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ConvertExtensionItemsDtoToOrderDetailsDtoQueryHandler> _logger;
        public ConvertExtensionItemsDtoToOrderDetailsDtoQueryHandler(IMapper mapper, ILogger<ConvertExtensionItemsDtoToOrderDetailsDtoQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<OrderDetailDto>? Items, string ErrorMessage)> Handle(ConvertExtensionItemsDtoToOrderDetailsDtoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var itemsDto = _mapper.Map<List<OrderDetailDto>>(request.entities);
                return (true, itemsDto, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while convert an object to OrderDetailDto.");
                return (false, null, "An error occurred while converting to OrderDetailDto.");
            }
        }
    }
}
