using AutoMapper;
using MediatR;
using SK.Visit.Application.Dtos;
using SK.Visit.Application.Exceptions;
using SK.Visit.Application.Interfaces;
using SK.Visit.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SK.Visit.Application.Features.Visit.AgentTrip.Commands
{
    public sealed record UpdateArrivalTimeCommand(TripDto Trip) : IRequest<TripDto>;

    public class UpdateArrivalTimeCommandHandler : IRequestHandler<UpdateArrivalTimeCommand, TripDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateArrivalTimeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TripDto> Handle(UpdateArrivalTimeCommand request, CancellationToken cancellationToken)
        {
            // Validate the trip entity before proceeding
            await ValidateTripUpdate(request.Trip);
            
            var trip = _mapper.Map<Destination>(request.Trip);
            var updated = await _unitOfWork.DestinationsRepository.UpdateArrivalTime(trip);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TripDto>(updated);
        }

        private async Task ValidateTripUpdate(TripDto trip)
        {
            
            // Validate Name
            if (string.IsNullOrEmpty(trip.ArrivalTime))
            {
                throw new ValidationException("Trip ArrivalTime is required.");
            }

            if (trip.Status != Enum.TripStatusDto.Ongoing)
            {
                throw new ValidationException("Trip Status Should be Ongoing.");
            }


        }
    }

}
