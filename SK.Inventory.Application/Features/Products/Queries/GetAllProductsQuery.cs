using AutoMapper;
using MediatR;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Interfaces;

namespace SK.Inventory.Application.Features.Products.Queries
{
    public sealed record GetAllProductsQuery() : IRequest<List<ProductDto>>;

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            if(!products.Any())
            {
                return new List<ProductDto>();
            }
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
