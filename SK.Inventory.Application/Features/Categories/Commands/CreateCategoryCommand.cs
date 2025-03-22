using AutoMapper;
using MediatR;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;



namespace SK.Inventory.Application.Features.Categories.Commands
{
    public sealed record CreateCategoryCommand(CategoryDto Category) : IRequest<CategoryDto>;
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryEntity = _mapper.Map<Category>(request.Category);
            categoryEntity = await _categoryRepository.CreateAsync(categoryEntity);

            // Map back to DTO after creation to include any updates (e.g., ID)
            return _mapper.Map<CategoryDto>(categoryEntity);
        }
    }
}
