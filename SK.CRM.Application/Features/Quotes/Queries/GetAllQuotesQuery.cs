using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Features.Quotes.Commands;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Quotes.Queries
{
    public sealed record GetAllQuotesQuery(string? userId = null) : IRequest<(bool IsSuccess, List<QuoteDto>? QuotesDto, string ErrorMessage)>;

    public class GetAllQuotesQueryHandler : IRequestHandler<GetAllQuotesQuery, (bool IsSuccess, List<QuoteDto>? QuotesDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllQuotesQueryHandler> _logger;
        public GetAllQuotesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllQuotesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<QuoteDto>? QuotesDto, string ErrorMessage)> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _unitOfWork.QuoteRepository.GetAllAsync(request.userId);
                if (!orders.Any())
                {
                    return (false, new List<QuoteDto>(), "No quotes found for the specified user.");
                }
                return (true, _mapper.Map<List<QuoteDto>>(orders), string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving quotes: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while retrieving quotes. Please try again later.");

            }
        }
    }
}
