namespace SK.Visit.Application.Dtos
{
    public class VisitPlanningDto
    {
        public AgentDto _Agent { get; set; }
        public List<DestinationDto> Destinations { get; set; }

        public List<DestinationDto> GetDestinationsBySelectedDate(DateTime selectedDate)
        {
            if (Destinations.Count == 0)
            {
                return new List<DestinationDto>() { new DestinationDto { IsFake = true, Date = selectedDate } };
            }

            if (Destinations.Where(v => v.Date == selectedDate).Count() == 0)
            {
                Destinations.RemoveAll(i => i.IsFake == true && i.Date != selectedDate);
                Destinations.Add(new DestinationDto { IsFake = true, Date = selectedDate });
                return Destinations.Where(v => v.Date == selectedDate).ToList();

            }
            return Destinations.Where(v => v.Date == selectedDate).ToList();
        }

        public int DestinationCountBySelectedDate(DateTime selectedDate)
        {
            return Destinations.Count(v => v.Date == selectedDate && v.IsFake == false);
        }

        public List<DestinationDto> AddDestination(DestinationDto destination,DateTime selectedDate)
        {
            if (!Destinations.Exists(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.Date == selectedDate))
            {
                if(Destinations.Exists(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.IsDelevery))
                {
                    Destinations.RemoveAll(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.IsDelevery);
                }
                destination.Date = selectedDate;
                Destinations.Add(destination);

                // this is to remove the fake destination if it exists (its a workarround solution for a problem with blazor-draganddrop)
                if (Destinations.Where(x => x.Date == selectedDate).Count() > 1)
                {
                    Destinations.RemoveAll(i => i.IsFake == true && i.Date == selectedDate);
                }
            }
            return Destinations;
        }
    }
}
