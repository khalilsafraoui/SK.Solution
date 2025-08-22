using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Application.Features.Products.Queries;
using SK.Inventory.Application.Interfaces;

namespace SK.Inventory.Application.Features.Categories.Queries
{
    public sealed record GetAllCategoriesQuery(int pageIndex = 0, int pageSize = 0) : IRequest<(bool IsSuccess, List<CategoryDto> ? CategoriesDto, int TotalCount, string ErrorMessage)>;

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, (bool IsSuccess, List<CategoryDto>? CategoriesDto, int TotalCount, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCategoriesQueryHandler> _logger;

        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllCategoriesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<CategoryDto>? CategoriesDto, int TotalCount, string ErrorMessage)> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.Categories.GetAllAsync(request.pageIndex, request.pageSize);
                if (!result.categories.Any())
                {
                    return (false, new List<CategoryDto>(), 0, "No categories found.");
                }
                var categoriesconvert = _mapper.Map<List<CategoryDto>>(result.categories);
                return (true, categoriesconvert, result.TotalCount, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving categories: {Message}", ex.Message);
                return (false, null, 0, "An unexpected error occurred while retrieving categories. Please try again later.");

            }
        }
    }
}
