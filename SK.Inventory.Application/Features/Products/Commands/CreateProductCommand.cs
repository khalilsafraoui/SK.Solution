using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Features.Products.Dtos;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;
using System.ComponentModel.DataAnnotations;


namespace SK.Inventory.Application.Features.Products.Commands
{
    public sealed record CreateProductCommand(Product_GeneralInformations Product) : IRequest<(bool IsSuccess, Product_GeneralInformations? GeneralInformationsDto, string ErrorMessage)>;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, (bool IsSuccess, Product_GeneralInformations? GeneralInformationsDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, Product_GeneralInformations? GeneralInformationsDto, string ErrorMessage)> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _mapper.Map<Product>(request.Product);
                // Validate the Product entity before proceeding
                await ValidateProduct(product);
                var updated = await _unitOfWork.Products.CreateAsync(product);
                await _unitOfWork.SaveChangesAsync();
                var result = _mapper.Map<Product_GeneralInformations>(updated);
                return (true, result, string.Empty);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error occurred while creating product: {Message}", ex.Message);
                return (false, null, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating product: {Message}", ex.Message);
                return (false, null, $"An error occurred while creating the product: {ex.Message}");
            }
        }

        private async Task ValidateProduct(Product product)
        {
            // Validate Name
            if (string.IsNullOrEmpty(product.Name))
            {
                throw new ValidationException("Product name is required.");
            }

            // Validate Price
            if (product.Price <= 0 || product.Price > 1000)
            {
                throw new ValidationException("Product price must be between 0.01 and 1000.");
            }

            // Validate CategoryId
            if (product.CategoryId <= 0)
            {
                throw new ValidationException("Category ID must be greater than 0.");
            }

            // Check if the Category exists in the database
            var category = await _unitOfWork.Categories.GetByIdAsync(product.CategoryId);
            if (category == null)
            {
                throw new ValidationException($"Category with ID {product.CategoryId} does not exist.");
            }
        }
    }
}
