using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Interfaces;

namespace SK.Inventory.Application.Features.Products.Queries
{
    public sealed record GetProductByIdQuery(int Id) : IRequest<(bool IsSuccess, ProductDto? ProductDto, string ErrorMessage)>;

    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, (bool IsSuccess, ProductDto? ProductDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetProductByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<(bool IsSuccess, ProductDto? ProductDto, string ErrorMessage)> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
                if (product == null)
                {
                    return (false, null, "Product not found.");
                }
                var productDto = _mapper.Map<ProductDto>(product);
                return (true, productDto, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving product by ID: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while retrieving the product. Please try again later.");
            }
        }
    }
}
