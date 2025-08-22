using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Exceptions;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;

namespace SK.Inventory.Application.Features.Categories.Commands
{
    public sealed record UpdateCategoryCommand(CategoryDto Category) : IRequest<(bool IsSuccess, CategoryDto? CategoryDto, string ErrorMessage)>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, (bool IsSuccess, CategoryDto? CategoryDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateCategoryCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, CategoryDto? CategoryDto, string ErrorMessage)> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(request.Category.Id);
                if (category == null)
                    return (false, null, $"Category with ID {request.Category.Id} not found.");
                            

                _mapper.Map(request.Category, category);

                var updated = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return (true, _mapper.Map<CategoryDto>(category), string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating category: {Message}", ex.Message);
                return (false, null, $"An error occurred while updating the category: {ex.Message}");
            }
        }
    }

}
