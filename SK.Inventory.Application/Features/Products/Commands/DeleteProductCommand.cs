using MediatR;
using SK.Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace SK.Inventory.Application.Features.Products.Commands
{
    public sealed record DeleteProductCommand(int ProductId) : IRequest<(bool IsSuccess,string ErrorMessage)>;


    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ILogger<DeleteProductCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
                if (product == null)
                {
                    return (false, "product_not_found"); //$"Product with ID {request.ProductId} not found."
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
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product with ID {ProductId}: {Message}", request.ProductId, ex.Message);
                return (false, "An_unexpected_error_occurred_while_deleting_the_product");//An unexpected error occurred while deleting the product. Please try again later.

            }
        }
    }

}
