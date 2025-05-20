namespace SK.Visit.Application.Dtos
{
    public class DestinationDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string OrderId { get; set; }

        public string CustomerId { get; set; }

        public string AddressId { get; set; }

        public bool IsDelevery { get; set; } = false;

        public string? CompanyName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Priority { get; set; }
        public DateTime Date { get; set; }

        public bool IsFake { get; set; } = false;

        public bool IsSelected { get; set; } = false;

        public int TotalVisitPlanned { get; set; } = 0;

        public MarkDto? Mark { get; set; }

        public string CityId { get; set; }

        public string StateId { get; set; }

        public string CountryId { get; set; }


        public DestinationDto Clone
        {
            get
            {
                return new DestinationDto
                {
                    OrderId = this.OrderId,
                    CustomerId = this.CustomerId,
                    IsDelevery = this.IsDelevery,
                    Name = this.Name,
                    Address = this.Address,
                    Phone = this.Phone,
                    Priority = this.Priority,
                    Date = this.Date,
                    IsFake = this.IsFake,
                    IsSelected = this.IsSelected,
                    TotalVisitPlanned = this.TotalVisitPlanned,
                    Mark = this.Mark != null ? new MarkDto(this.Mark.Lat, this.Mark.Lng, this.Mark.Title, this.Mark.Label) : null,
                    CityId = this.CityId,
                    StateId = this.StateId,
                    CountryId = this.CountryId,
                    AddressId = this.AddressId

                };
            }
        }
    }
}
