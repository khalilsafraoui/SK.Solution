using MediatR;
using SK.Inventory.Application.Interfaces;


namespace SK.Inventory.Application.Features.Categories.Commands
{
    public sealed record DeleteCategoryCommand(int CategoryId) : IRequest<bool>;


    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                return false; // Or throw an exception if preferred
            }

            await _unitOfWork.Categories.DeleteAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }

}
