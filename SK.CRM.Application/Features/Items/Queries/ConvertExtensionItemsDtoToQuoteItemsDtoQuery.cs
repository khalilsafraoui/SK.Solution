using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Features.Items.Dtos;

namespace SK.CRM.Application.Features.Items.Queries
{
    

    public sealed record ConvertExtensionItemsDtoToQuoteItemsDtoQuery(List<ExtensionItemDto> entities) : IRequest<(bool IsSuccess, List<QuoteItemDto>? Items, string ErrorMessage)>;

    public class ConvertExtensionItemsDtoToQuoteItemsDtoQueryHandler : IRequestHandler<ConvertExtensionItemsDtoToQuoteItemsDtoQuery, (bool IsSuccess, List<QuoteItemDto>? Items, string ErrorMessage)>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ConvertExtensionItemsDtoToQuoteItemsDtoQueryHandler> _logger;
        public ConvertExtensionItemsDtoToQuoteItemsDtoQueryHandler(IMapper mapper, ILogger<ConvertExtensionItemsDtoToQuoteItemsDtoQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<QuoteItemDto>? Items, string ErrorMessage)> Handle(ConvertExtensionItemsDtoToQuoteItemsDtoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                    var itemsDto = _mapper.Map<List<QuoteItemDto>>(request.entities);
                    return (true, itemsDto, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while convert an object to QuoteItemDto.");
                return (false, null, "An error occurred while converting to QuoteItemDto.");
            }
        }
    }
}
