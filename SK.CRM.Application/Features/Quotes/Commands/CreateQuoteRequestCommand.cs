using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities.Quote;
using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Application.Features.Quotes.Commands
{
    public sealed record CreateQuoteRequestCommand(QuoteDto Quote) : IRequest<(bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)>;
    public class CreateQuoteRequestCommandHandler : IRequestHandler<CreateQuoteRequestCommand, (bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateQuoteRequestCommandHandler> _logger;
        public CreateQuoteRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateQuoteRequestCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)> Handle(CreateQuoteRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var quote = _mapper.Map<Quote>(request.Quote);
                await ValidateQuote(quote);
                quote = await _unitOfWork.QuoteRepository.CreateAsync(quote);
                await _unitOfWork.SaveChangesAsync();
                // Map back to DTO after creation to include any updates (e.g., ID)
                return (true,_mapper.Map<QuoteDto>(quote),string.Empty);
            }
            catch (ValidationException valEx)
            {
                return (false, null, $"Validation failed: {valEx.Message}");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while creating the quote: {Message}", ex.Message);
                return (false, null,"An unexpected error occurred while creating the quote. Please try again later.");
            }
        }

        private async Task ValidateQuote(Quote quote)
        {
            if (!quote.Items.Any())
            {
                throw new ValidationException("Items are required.");
            }
           
        }
    }
}
