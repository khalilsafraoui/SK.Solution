using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Features.Products.Commands;
using SK.Inventory.Application.Features.Products.Dtos;
using SK.Inventory.Application.Interfaces;

namespace SK.Inventory.Application.Features.Products.Queries
{
    public sealed record GetProduct_GeneralInformationsByIdQuery(int Id) : IRequest<(bool IsSuccess,Product_GeneralInformations? product_GeneralInformations, string ErrorMessage)>;

    public sealed class GetProduct_GeneralInformationsByIdQueryHandler : IRequestHandler<GetProduct_GeneralInformationsByIdQuery, (bool IsSuccess, Product_GeneralInformations? product_GeneralInformations, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProduct_GeneralInformationsByIdQueryHandler> _logger;

        public GetProduct_GeneralInformationsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetProduct_GeneralInformationsByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<(bool IsSuccess, Product_GeneralInformations? product_GeneralInformations, string ErrorMessage)> Handle(GetProduct_GeneralInformationsByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
                if (product is null)
                {
                    _logger.LogWarning("Product with ID {Id} not found.", request.Id);
                    return (false, null, "Product not found.");
                }
                var product_GeneralInformations = _mapper.Map<Product_GeneralInformations>(product);
                return (true, product_GeneralInformations, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving product with ID {Id}: {Message}", request.Id, ex.Message);
                return (false, null, $"An error occurred while retrieving the product: {ex.Message}");
            }
        }
    }
}
