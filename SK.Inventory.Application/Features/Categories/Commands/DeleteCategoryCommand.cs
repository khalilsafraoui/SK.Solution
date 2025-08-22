using MediatR;
using Microsoft.Extensions.Logging;
using SK.Inventory.Application.Features.Products.Commands;
using SK.Inventory.Application.Interfaces;


namespace SK.Inventory.Application.Features.Categories.Commands
{
    public sealed record DeleteCategoryCommand(int CategoryId) : IRequest<(bool IsSuccess, string ErrorMessage)>;


    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCategoryCommandHandler> _logger;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteCategoryCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
                if (category == null)
                {
                    return (false, "category_not_found"); ; // Or throw an exception if preferred
                }

                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return (true,string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting category with ID {CategoryId}: {Message}", request.CategoryId, ex.Message);
                return (false, "An_unexpected_error_occurred_while_deleting_the_category");//An unexpected error occurred while deleting the product. Please try again later.
            }

        }
    }

}
