using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Interfaces;

namespace SK.Inventory.Application.Features.Products.Queries
{
    public sealed record GetAllProductsQuery(int pageIndex = 0, int pageSize = 0) : IRequest<(bool IsSuccess, List<ProductDto> ? ProductsDto, int TotalCount, string ErrorMessage)>;

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, (bool IsSuccess, List<ProductDto>? ProductsDto, int TotalCount, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductsQueryHandler> _logger;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllProductsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<ProductDto>? ProductsDto, int TotalCount, string ErrorMessage)> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.Products.GetAllAsync(request.pageIndex, request.pageSize);
                if (!result.products.Any())
                {
                    return (false, new List<ProductDto>(), 0, "No products found.");
                }
                var productconvert = _mapper.Map<List<ProductDto>>(result.products);
                return (true, productconvert, result.TotalCount, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving products: {Message}", ex.Message);
                return (false, null, 0, "An unexpected error occurred while retrieving products. Please try again later.");

            }
        }
    }
}
