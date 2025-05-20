

using AutoMapper;
using MediatR;
using SK.Visit.Application.Dtos;
using SK.Visit.Application.Interfaces;
using SK.Visit.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SK.Visit.Application.Features.Visit.AgentTrip.Commands
{
    public sealed record UpdateSkippedPermanentlyCommand(TripDto Trip) : IRequest<TripDto>;

    public class UpdateSkippedPermanentlyCommandHandler : IRequestHandler<UpdateSkippedPermanentlyCommand, TripDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSkippedPermanentlyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TripDto> Handle(UpdateSkippedPermanentlyCommand request, CancellationToken cancellationToken)
        {
            // Validate the trip entity before proceeding
            await ValidateTripUpdate(request.Trip);

            var trip = _mapper.Map<Destination>(request.Trip);
            var updated = await _unitOfWork.DestinationsRepository.UpdateTripSkippedPermanently(trip);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TripDto>(updated);
        }

        private async Task ValidateTripUpdate(TripDto trip)
        {
            if (string.IsNullOrEmpty(trip.SkipReason))
            {
                throw new ValidationException("Trip SkipReason is required.");
            }

            if (trip.Status != Enum.TripStatusDto.SkippedPermanently)
            {
                throw new ValidationException("Trip Status Should be SkippedPermanently.");
            }


        }
    }
}
