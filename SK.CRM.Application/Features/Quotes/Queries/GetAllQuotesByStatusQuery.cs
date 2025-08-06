using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Features.Quotes.Commands;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Quotes.Queries
{
    public sealed record GetAllQuotesByStatusQuery(string[] statuses,int pageIndex = 0,int pageSize = 0, string? userId = null) : IRequest<(bool IsSuccess, List<QuoteDto>? QuotesDto, int TotalCount, string ErrorMessage)>;

    public class GetAllQuotesByStatusQueryHandler : IRequestHandler<GetAllQuotesByStatusQuery, (bool IsSuccess, List<QuoteDto>? QuotesDto, int TotalCount, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllQuotesByStatusQueryHandler> _logger;
        public GetAllQuotesByStatusQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllQuotesByStatusQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<QuoteDto>? QuotesDto, int TotalCount, string ErrorMessage)> Handle(GetAllQuotesByStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.QuoteRepository.GetByStatusesAsync(request.statuses, request.pageIndex, request.pageSize, request.userId);
                if (!result.Quotes.Any())
                {
                    return (false, new List<QuoteDto>(),0, "0 quotes found");
                }
                return (true, _mapper.Map<List<QuoteDto>>(result.Quotes), result.TotalCount, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving quotes: {Message}", ex.Message);
                return (false, null, 0, "An unexpected error occurred while retrieving quotes. Please try again later.");

            }
        }
    }
}
