using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities.Quote;
using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Application.Features.Quotes.Commands
{
   

    public sealed record UpdateQuoteRequestCommand(QuoteDto QuoteDto) : IRequest<(bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)>;

    public class UpdateQuoteRequestCommandHandler : IRequestHandler<UpdateQuoteRequestCommand, (bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuoteRequestCommandHandler> _logger;
        public UpdateQuoteRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateQuoteRequestCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)> Handle(UpdateQuoteRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var quote = _mapper.Map<Quote>(request.QuoteDto);
                await ValidateQuote(quote);
                var IsUpdated = await _unitOfWork.QuoteRepository.UpdateAsync(quote);
                foreach (var item in quote.Items)
                {
                    item.QuoteId = quote.Id; // Ensure each item is linked to the quote
                }
                await _unitOfWork.QuoteItemRepository.UpdateItemsAsync(quote.Id, quote.Items);
                await _unitOfWork.SaveChangesAsync();

                if (!IsUpdated)
                {
                    return (false, null, "Failed to update quote. Please try again later.");
                }

                return (true, _mapper.Map<QuoteDto>(quote), string.Empty);
            }
            catch (ValidationException valEx)
            {
                return (false, null, $"Validation failed: {valEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the quote: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while updating the quote. Please try again later.");
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
