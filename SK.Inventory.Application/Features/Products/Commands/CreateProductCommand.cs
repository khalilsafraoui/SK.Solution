using AutoMapper;
using MediatR;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;


namespace SK.Inventory.Application.Features.Products.Commands
{
    public sealed record CreateProductCommand(ProductDto Product) : IRequest<ProductDto>;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.Product);
            product = await _productRepository.CreateAsync(product);

            // Map back to DTO after creation to include any updates (e.g., ID)
            return _mapper.Map<ProductDto>(product);
        }
    }
}
