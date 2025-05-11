using MediatR;
using SK.Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace SK.Inventory.Application.Features.Products.Commands
{
    public sealed record DeleteProductCommand(int ProductId) : IRequest<bool>;


    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                return false; // Or throw an exception if preferred
            }

            await _unitOfWork.Products.DeleteAsync(product);

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }

}
