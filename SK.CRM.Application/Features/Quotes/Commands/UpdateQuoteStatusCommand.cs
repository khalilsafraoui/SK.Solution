using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Application.Features.Quotes.Commands
{
   

    public sealed record UpdateQuoteStatusCommand(Guid quoteId, string status) : IRequest<(bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)>;

    public class UpdateQuoteStatusCommandHandler : IRequestHandler<UpdateQuoteStatusCommand, (bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuoteStatusCommandHandler> _logger;
        public UpdateQuoteStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateQuoteStatusCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, QuoteDto? QuoteDto, string ErrorMessage)> Handle(UpdateQuoteStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var quote = await _unitOfWork.QuoteRepository.UpdateStatusAsync(request.quoteId, request.status);
                            
                await _unitOfWork.SaveChangesAsync();

                if (quote is null)
                {
                    return (false, null, "Failed to update quote status. Please try again later.");
                }

                return (true, _mapper.Map<QuoteDto>(quote), string.Empty);
            }
            catch (ValidationException valEx)
            {
                return (false, null, $"Validation failed: {valEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the quote status: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while updating the quote status. Please try again later.");
            }
        }
    }
}
