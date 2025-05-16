namespace SK.Visit.UI.Blazor.Model
{
    public class VisitPlanning
    {
        public Agent _Agent { get; set; }
        public List<Destination> Destinations { get; set; }

        public List<Destination> GetDestinationsBySelectedDate(DateTime selectedDate)
        {
            if (Destinations.Count == 0)
            {
                return new List<Destination>() { new Destination { IsFake = true, Date = selectedDate } };
            }

            if (Destinations.Where(v => v.Date == selectedDate).Count() == 0)
            {
                Destinations.RemoveAll(i => i.IsFake == true && i.Date != selectedDate);
                Destinations.Add(new Destination { IsFake = true, Date = selectedDate });
                return Destinations.Where(v => v.Date == selectedDate).ToList();

            }
            return Destinations.Where(v => v.Date == selectedDate).ToList();
        }

        public int DestinationCountBySelectedDate(DateTime selectedDate)
        {
            return Destinations.Count(v => v.Date == selectedDate && v.IsFake == false);
        }

        public List<Destination> AddDestination(Destination destination,DateTime selectedDate)
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
