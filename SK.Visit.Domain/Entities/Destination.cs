namespace SK.Visit.Domain.Entities
{
    public class Destination : BaseEntity
    {
        public string AgentId { get; set; }
        public string? OrderId { get; set; }

        public string CustomerId { get; set; }

        public string AddressId { get; set; }

        public bool IsDelevery { get; set; } = false;
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        
        public DateTime Date { get; set; }

        public string CityId { get; set; }

        public string StateId { get; set; }

        public string CountryId { get; set; }
    }
}
