using SK.Visit.Application.Dtos;
using SK.Visit.UI.Blazor.Model;


namespace SK.Visit.UI.Blazor.Services
{
    public class VisitPlanningServices : IVisitPlanningServices
    {
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

        public List<DestinationDto> AddDestination(List<VisitPlanningDto> visitPlannings, List<DestinationDto> Destinations, VisitPlanningDto visitPlanning, DestinationDto destination, DateTime selectedDate)
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

        public bool DeleteVisitPlanned(List<VisitPlanningDto> visitPlannings, List<DestinationDto> Destinations, VisitPlanningDto visitPlanning, DestinationDto destination, DateTime selectedDate)
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
