using AutoMapper;
using Microsoft.Extensions.Logging;
using SK.Inventory.Application.Interfaces;
using SK.Solution.Shared.Interfaces.Inventory.Product;
using SK.Solution.Shared.Model.Inventory.Product;

namespace SK.Inventory.Application.Features.Products.Services
{
    public class SharedProductServices : ISharedProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SharedProductServices> _logger;
        public SharedProductServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SharedProductServices> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, List<SharedProductForCrmDto>? Products, string ErrorMessage)> GetProductForCrmUseAsync()
        {
            try
            {
                var result = await _unitOfWork.Products.GetAllAsync();
                if (result.TotalCount == 0)
                {
                    return (false, null, "No products found.");
                }
                var productDtos = _mapper.Map<List<SharedProductForCrmDto>>(result.products);
                return (true, productDtos, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while retrieving products for CRM use: {Message}", ex.Message);
                return (false, null, ex.Message);
            }
        }

       
    }
}
