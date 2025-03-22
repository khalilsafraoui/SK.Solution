using AutoMapper;
using MediatR;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Interfaces;

namespace SK.Inventory.Application.Features.Products.Queries
{
    public sealed record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;

    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            return product is null ? null : _mapper.Map<ProductDto>(product);
        }
    }
}
