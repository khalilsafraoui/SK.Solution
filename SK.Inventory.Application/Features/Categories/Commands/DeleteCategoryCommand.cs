using MediatR;
using SK.Inventory.Application.Interfaces;


namespace SK.Inventory.Application.Features.Categories.Commands
{
    public sealed record DeleteCategoryCommand(int CategoryId) : IRequest<bool>;


    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                return false; // Or throw an exception if preferred
            }

            await _categoryRepository.DeleteAsync(request.CategoryId);
            return true;
        }
    }

}
