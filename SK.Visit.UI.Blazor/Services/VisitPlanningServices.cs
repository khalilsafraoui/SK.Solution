using SK.Solution.Shared.Interfaces.Crm;
using SK.Solution.Shared.Interfaces.Identity;
using SK.Solution.Shared.Model.Crm;
using SK.Solution.Shared.Model.Identity;
using SK.Visit.Application.Dtos;
using SK.Visit.UI.Blazor.Model;


namespace SK.Visit.UI.Blazor.Services
{
    public class VisitPlanningServices : IVisitPlanningServices
    {

        private readonly ISharedUserServices _sharedUserServices;
        private readonly ISharedCustomerServices _sharedCustomerServices;
        private readonly ISharedOrderServices _sharedOrderServices;


        public VisitPlanningServices(ISharedUserServices sharedUserServices, ISharedCustomerServices sharedCustomerServices, ISharedOrderServices sharedOrderServices)
        {
            _sharedUserServices = sharedUserServices;
            _sharedCustomerServices = sharedCustomerServices;
            _sharedOrderServices = sharedOrderServices;
        }
        //private List<VisitPlanningDto> visitPlannings = new()
        //{
        //};

        private List<DestinationDto> Destinations = new()
        {
        };

        private List<KeyValueAddress> Countries = new()
        {
             new KeyValueAddress { _key = "TN", _value = "Tunisia" },
             new KeyValueAddress { _key = "FR", _value = "France" },
             new KeyValueAddress { _key = "BL", _value = "Belgium" },
        };
        private List<KeyValueAddress> States = new()
        {
            new KeyValueAddress { _key = "BA", _value = "Bizerte" },
            new KeyValueAddress { _key = "BB", _value = "Bordj Bou Arreridj" },
            new KeyValueAddress { _key = "BZ", _value = "Beja" },
            new KeyValueAddress { _key = "BN", _value = "Ben Arous" },
            new KeyValueAddress { _key = "BS", _value = "Béjaïa" },
            new KeyValueAddress { _key = "NA", _value = "Namur" },

        };
        private List<KeyValueAddress> Cities = new()
        { 
            new KeyValueAddress { _key = "1", _value = "Tunis" },
            new KeyValueAddress { _key = "2", _value = "Sousse" },
            new KeyValueAddress { _key = "3", _value = "Sfax" },
            new KeyValueAddress { _key = "4", _value = "Kairouan" },
            new KeyValueAddress { _key = "5", _value = "Nabeul" },
            new KeyValueAddress { _key = "6", _value = "Gabès" },
            new KeyValueAddress { _key = "7", _value = "Monastir" },
            new KeyValueAddress { _key = "NA", _value = "Namur" },
        };

        public List<DestinationDto> GetDestinationsBySelectedDate(VisitPlanningDto visitPlanning, DateTime selectedDate)
        {
            if (visitPlanning.Destinations.Count == 0)
            {
                return new List<DestinationDto>() { new DestinationDto { IsFake = true, Date = selectedDate } };
            }

            if (visitPlanning.Destinations.Where(v => v.Date == selectedDate).Count() == 0)
            {
                visitPlanning.Destinations.RemoveAll(i => i.IsFake == true && i.Date != selectedDate);
                visitPlanning.Destinations.Add(new DestinationDto { IsFake = true, Date = selectedDate });
                return visitPlanning.Destinations.Where(v => v.Date == selectedDate).ToList();

            }
            return visitPlanning.Destinations.Where(v => v.Date == selectedDate).ToList();
        }

        public int DestinationCountBySelectedDate(VisitPlanningDto visitPlanning, DateTime selectedDate)
        {
            return visitPlanning.Destinations.Count(v => v.Date == selectedDate && v.IsFake == false);
        }

        public List<DestinationDto> AddDestination(List<VisitPlanningDto> visitPlannings, VisitPlanningDto visitPlanning, DestinationDto destination, DateTime selectedDate)
        {
            VisitPlanningDto _visitPlanning = visitPlannings.First(i => i._Agent.Id == visitPlanning._Agent.Id);




            if (!_visitPlanning.Destinations.Exists(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.AddressId == destination.AddressId && i.Date == selectedDate))
            {
                if(visitPlannings.Exists(i => i.Destinations.Exists(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.AddressId == destination.AddressId && i.IsDelevery)))
                {
                   foreach(VisitPlanningDto visitPlanningToPerformeRemoe in visitPlannings.Where(i => i.Destinations.Exists(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.AddressId == destination.AddressId && i.IsDelevery)))
                    {
                        visitPlanningToPerformeRemoe.Destinations.RemoveAll(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.AddressId == destination.AddressId && i.IsDelevery);
                    }
                }
                DestinationDto newDestination  = destination.Clone;
                newDestination.Date = selectedDate;
                _visitPlanning.Destinations.Add(newDestination);

                Destinations.Where(i => i.CustomerId == destination.CustomerId && i.OrderId == destination.OrderId && i.AddressId == destination.AddressId).ToList().ForEach(i => {
                    i.IsSelected = true;
                    i.TotalVisitPlanned = IncrementVisitPlannedCount(i);
                });

                // this is to remove the fake destination if it exists (its a workarround solution for a problem with blazor-draganddrop)
                if (_visitPlanning.Destinations.Where(x => x.Date == selectedDate).Count() > 1)
                {
                    _visitPlanning.Destinations.RemoveAll(i => i.IsFake == true && i.Date == selectedDate);
                }
            }
            return visitPlanning.Destinations;
        }

        private int IncrementVisitPlannedCount(DestinationDto dist)
        {
            if (dist.TotalVisitPlanned == 0 || !dist.IsDelevery)
                dist.TotalVisitPlanned++;
            return dist.TotalVisitPlanned;
        }

        private int DecrementVisitPlannedCount(DestinationDto dist)
        {
            if (dist.TotalVisitPlanned > 0)
                dist.TotalVisitPlanned--;
            return dist.TotalVisitPlanned;
        }
        private bool IsSelecedCheckBeforeDelete(DestinationDto dist)
        {
            if (dist.TotalVisitPlanned == 1)
                return false;
            return true;
        }

        public static string GetRandomHexColor()
        {
            var random = new Random();
            return $"#{random.Next(0x1000000):X6}"; // Generates something like "#A1B2C3"
        }


        public async Task<List<DestinationDto>> GetDistinations()
        {
            List<SharedCustomerDestinationDto> customers = await _sharedCustomerServices.GetCusomersDestinationsAsync();
            List<SharedOrderDestinationDto> orders = await _sharedOrderServices.GetOrdersDestinaionsAsync();

            Destinations = new();
            foreach (var customer in customers)
            {
                Destinations.Add(new DestinationDto
                {
                    CustomerId = customer.CustomerId?.ToString(),
                    AddressId = customer.AddressId?.ToString(),
                    Name = customer.Name,
                    Address = customer.Address,
                    Phone = customer.Phone,
                    Mark =  customer.Mark != null ? new MarkDto(customer.Mark.Lat, customer.Mark.Lng, customer.Mark.Title, customer.Mark.Label) :null,
                    CountryId = customer.CountryId,
                    StateId = customer.StateId,
                    CityId = customer.CityId
                });
            }
            foreach (var order in orders)
            {
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
                    CityId = order.CityId
                });
            }

            return Destinations;
        }

        public List<KeyValueAddress> GetCountries()
        {
            return Countries;
        }

        public List<KeyValueAddress> GetStates()
        {
            return States;
        }

        public List<KeyValueAddress> GetCities()
        {
            return Cities;
        }

        public bool DeleteVisitPlanned(List<VisitPlanningDto> visitPlannings, VisitPlanningDto visitPlanning, DestinationDto destination, DateTime selectedDate)
        {
            VisitPlanningDto _visitPlanning = visitPlannings.First(i => i._Agent.Id == visitPlanning._Agent.Id);
            if (_visitPlanning.Destinations.Exists(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.AddressId == destination.AddressId && i.Date == selectedDate))
            {
                _visitPlanning.Destinations.RemoveAll(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.AddressId == destination.AddressId && i.Date == selectedDate);
                Destinations.Where(i => i.CustomerId == destination.CustomerId && i.OrderId == destination.OrderId && i.AddressId == destination.AddressId).ToList().ForEach(i => {
                    i.IsSelected = IsSelecedCheckBeforeDelete(i);
                    i.TotalVisitPlanned = DecrementVisitPlannedCount(i);
                });
                return true;
            }
            return false;
        }
    }
}
