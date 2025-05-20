using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SK.Solution.Shared.Model.Crm;
using SK.Visit.Application.Dtos;
using SK.Visit.Application.Interfaces;
using SK.Visit.Application.Settings;
using SK.Visit.Domain.Entities;
using System.Runtime;

namespace SK.Visit.Application.Features.Visit.Schedule.Queries
{
    public sealed record GetAllDestinationsQuery() : IRequest<List<DestinationDto>>;

    public class GetAllDestinationsQueryHandler : IRequestHandler<GetAllDestinationsQuery, List<DestinationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly VisitSettings _settings;

        public GetAllDestinationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IOptions<VisitSettings> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _settings = options.Value;
        }

        public async Task<List<DestinationDto>> Handle(GetAllDestinationsQuery request, CancellationToken cancellationToken)
        {
            var tomorrow = DateTime.Now.AddDays(_settings.NumberOfDaysToAddInScheduleGetAndSave).Date;
            var savedDestinations = await _unitOfWork.DestinationsRepository.GetDestinationsStartingFromSpecificDateAsync(tomorrow);
            if (!savedDestinations.Any())
            {
                savedDestinations = new List<Destination>();
            }


            List<SharedCustomerDestinationDto> customers = await _unitOfWork.SharedCustomerServices.GetCusomersDestinationsAsync();
            List<SharedOrderDestinationDto> orders = await _unitOfWork.SharedOrderServices.GetOrdersDestinaionsAsync();

            List<DestinationDto> Destinations = new List<DestinationDto>();
            foreach (var customer in customers)
            {
                int totalVisitPlanned = savedDestinations.Where(x => x.AddressId == customer.AddressId?.ToString() && x.CustomerId == customer.CustomerId?.ToString() && x.OrderId == null).Count();
                Destinations.Add(new DestinationDto
                {
                    CustomerId = customer.CustomerId?.ToString(),
                    AddressId = customer.AddressId?.ToString(),
                    Name = customer.Name,
                    Address = customer.Address,
                    Phone = customer.Phone,
                    Mark = customer.Mark != null ? new MarkDto(customer.Mark.Lat, customer.Mark.Lng, customer.Mark.Title, customer.Mark.Label) : null,
                    CountryId = customer.CountryId,
                    StateId = customer.StateId,
                    CityId = customer.CityId,
                    TotalVisitPlanned = totalVisitPlanned,
                    IsSelected = totalVisitPlanned > 0,
                });
            }
            foreach (var order in orders)
            {
                int totalVisitPlanned = savedDestinations.Where(x => x.AddressId == order.AddressId?.ToString() && x.CustomerId == order.CustomerId?.ToString() && x.OrderId == order.OrderId?.ToString()).Count();
                Destinations.Add(new DestinationDto
                {
                    OrderId = order.OrderId?.ToString(),
                    CustomerId = order.CustomerId?.ToString(),
                    AddressId = order.AddressId?.ToString(),
                    IsDelevery = order.IsDelevery,
                    Name = order.Name,
                    Address = order.Address,
                    Phone = order.Phone,
                    Mark = order.Mark != null ? new MarkDto(order.Mark.Lat, order.Mark.Lng, order.Mark.Title, order.Mark.Label) : null,
                    CountryId = order.CountryId,
                    StateId = order.StateId,
                    CityId = order.CityId,
                    TotalVisitPlanned = totalVisitPlanned,
                    IsSelected = totalVisitPlanned > 0,
                });
            }

            return Destinations;



           // return _mapper.Map<List<DestinationDto>>(destinations);
        }
    }
}
