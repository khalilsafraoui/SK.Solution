using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Quotes.Queries
{
    public sealed record GetQuoteByIdQuery(Guid Id) : IRequest<(bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)>;

    public sealed class GetQuoteByIdQueryHandler : IRequestHandler<GetQuoteByIdQuery, (bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllQuotesQueryHandler> _logger;
        public GetQuoteByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllQuotesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var quote = await _unitOfWork.QuoteRepository.GetByIdAsync(request.Id);
                if (quote == null)
                {
                    return (false, null, "Quote not found.");
                }
                // Map the quote entity to QuoteDto
                return (true, _mapper.Map<QuoteDto>(quote),string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving the quote: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while retrieving the quote. Please try again later.");
            }
        }
    }
}
