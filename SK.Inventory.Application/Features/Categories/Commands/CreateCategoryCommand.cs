using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;
using System.ComponentModel.DataAnnotations;



namespace SK.Inventory.Application.Features.Categories.Commands
{
    public sealed record CreateCategoryCommand(CategoryDto Category) : IRequest<(bool IsSuccess, CategoryDto? CategoryDto,string ErrorMessage)>;
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, (bool IsSuccess, CategoryDto? CategoryDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCategoryCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, CategoryDto? CategoryDto, string ErrorMessage)> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(request.Category);
                categoryEntity = await _unitOfWork.Categories.CreateAsync(categoryEntity);
                await _unitOfWork.SaveChangesAsync();
                // Map back to DTO after creation to include any updates (e.g., ID)
                return (true, _mapper.Map<CategoryDto>(categoryEntity), string.Empty);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error occurred while creating category: {Message}", ex.Message);
                return (false, null, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating category: {Message}", ex.Message);
                return (false, null, $"An error occurred while creating the category: {ex.Message}");
            }
        }
    }
}
