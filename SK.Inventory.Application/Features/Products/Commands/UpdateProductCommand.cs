using AutoMapper;
using MediatR;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Exceptions;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;
using System.ComponentModel.DataAnnotations;

namespace SK.Inventory.Application.Features.Products.Commands
{
    public sealed record UpdateProductCommand(ProductDto Product) : IRequest<ProductDto>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Product.Id)
                            ?? throw new NotFoundException(nameof(Product), request.Product.Id);

            _mapper.Map(request.Product, product);
            // Validate the Product entity before proceeding
            await ValidateProduct(product);
            var updated = await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
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
