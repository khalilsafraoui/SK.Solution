using SK.Visit.Application.Dtos;

namespace SK.Visit.UI.Blazor.Services
{
    public class VisitPlanningServices : IVisitPlanningServices
    {
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
