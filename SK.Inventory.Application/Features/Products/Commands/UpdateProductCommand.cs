using AutoMapper;
using MediatR;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Exceptions;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;

namespace SK.Inventory.Application.Features.Products.Commands
{
    public sealed record UpdateProductCommand(ProductDto Product) : IRequest<ProductDto>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Product.Id)
                            ?? throw new NotFoundException(nameof(Product), request.Product.Id);

            _mapper.Map(request.Product, product);

            var updated = await _productRepository.UpdateAsync(product);

            return _mapper.Map<ProductDto>(product);
        }
    }

}
