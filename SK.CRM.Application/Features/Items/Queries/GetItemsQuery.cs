using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.Features.Items.Dtos;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Items.Queries
{
    public sealed record GetItemsQuery() : IRequest<(bool IsSuccess, List<ItemDto>? Items, string ErrorMessage)>;

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, (bool IsSuccess, List<ItemDto>? Items, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetItemsQueryHandler> _logger;
        public GetItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetItemsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<ItemDto>? Items, string ErrorMessage)> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.SharedProductServices.GetProductForCrmUseAsync();
                if (result.IsSuccess)
                {
                    var itemsDto = _mapper.Map<List<ItemDto>>(result.Products);
                    return (true, itemsDto, string.Empty);
                }
                else
                {
                    return (false, null, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting Items from SharedProductServices.");
                return (false, null, "An error occurred while retrieving Items.");
            }
        }
    }
}
