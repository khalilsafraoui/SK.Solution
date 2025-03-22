using MediatR;
using SK.Inventory.Application.Interfaces;


namespace SK.Inventory.Application.Features.Products.Commands
{
    public sealed record DeleteProductCommand(int ProductId) : IRequest<bool>;


    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                return false; // Or throw an exception if preferred
            }

            await _productRepository.DeleteAsync(request.ProductId);
            return true;
        }
    }

}
