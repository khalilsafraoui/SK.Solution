using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Features.Items.Dtos;

namespace SK.CRM.Application.Features.Items.Queries
{
    

    public sealed record ConvertToExtensionItemsDtoQuery(object entities) : IRequest<(bool IsSuccess, List<ExtensionItemDto>? Items, string ErrorMessage)>;

    public class ConvertToExtensionItemsDtoQueryHandler : IRequestHandler<ConvertToExtensionItemsDtoQuery, (bool IsSuccess, List<ExtensionItemDto>? Items, string ErrorMessage)>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ConvertToExtensionItemsDtoQueryHandler> _logger;
        public ConvertToExtensionItemsDtoQueryHandler(IMapper mapper, ILogger<ConvertToExtensionItemsDtoQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<ExtensionItemDto>? Items, string ErrorMessage)> Handle(ConvertToExtensionItemsDtoQuery request, CancellationToken cancellationToken)
        {
            try
            {

                if(request.entities is List<QuoteItemDto> quoteItems)
                {
                    var itemsDto = _mapper.Map<List<ExtensionItemDto>>(quoteItems);
                    return (true, itemsDto, string.Empty);
                }
                else if (request.entities is List<OrderDetailDto> orderItems)
                {
                    var itemsDto = _mapper.Map<List<ExtensionItemDto>>(orderItems);
                    return (true, itemsDto, string.Empty);
                }
                return (false, null, "Unsupported entity type provided to ConvertToItemsQuery.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while convert an object to Items.");
                return (false, null, "An error occurred while convert Items.");
            }
        }
    }
}
