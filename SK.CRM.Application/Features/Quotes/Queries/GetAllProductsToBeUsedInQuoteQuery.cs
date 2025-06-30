using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Quotes.Queries
{
    public sealed record GetAllProductsToBeUsedInQuoteQuery() : IRequest<(bool IsSuccess, List<Quote_ProductsDto>? Products, string ErrorMessage)>;

    public class GetAllProductsToBeUsedInQuoteQueryHandler : IRequestHandler<GetAllProductsToBeUsedInQuoteQuery, (bool IsSuccess, List<Quote_ProductsDto>? Products, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductsToBeUsedInQuoteQueryHandler> _logger;
        public GetAllProductsToBeUsedInQuoteQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllProductsToBeUsedInQuoteQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<Quote_ProductsDto>? Products, string ErrorMessage)> Handle(GetAllProductsToBeUsedInQuoteQuery request, CancellationToken cancellationToken)
        {
            try
            {
              var result = await _unitOfWork.SharedProductServices.GetProductForCrmUseAsync();
                if (result.IsSuccess)
                {
                    var productsDto = _mapper.Map<List<Quote_ProductsDto>>(result.Products);
                    return (true, productsDto, string.Empty);
                }
                else
                {
                    return (false, null, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products for quote.");
                return (false, null, "An error occurred while retrieving products for the quote.");
            }
        }
    }
}
