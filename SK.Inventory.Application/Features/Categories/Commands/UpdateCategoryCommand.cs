using AutoMapper;
using MediatR;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Exceptions;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;

namespace SK.Inventory.Application.Features.Categories.Commands
{
    public sealed record UpdateCategoryCommand(CategoryDto Category) : IRequest<CategoryDto>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Category.Id)
                            ?? throw new NotFoundException(nameof(Category), request.Category.Id);

            _mapper.Map(request.Category, category);

            var updated = await _categoryRepository.UpdateAsync(category);

            return _mapper.Map<CategoryDto>(category);
        }
    }

}
