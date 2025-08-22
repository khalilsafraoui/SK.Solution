using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Interfaces;

namespace SK.Inventory.Application.Features.Categories.Queries
{
    public sealed record GetCategoryByIdQuery(int Id) : IRequest<(bool IsSuccess, CategoryDto? CategoryDto, string ErrorMessage)>;

    public sealed class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, (bool IsSuccess, CategoryDto? CategoryDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCategoryByIdQueryHandler> _logger;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetCategoryByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<(bool IsSuccess, CategoryDto? CategoryDto, string ErrorMessage)> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
                if (category == null)
                {
                    return (false, null, "Category not found.");
                }
                var categoryDto = _mapper.Map<CategoryDto>(category);
                return (true, categoryDto, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving category by ID: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while retrieving the category. Please try again later.");
            }
        }
    }
}
