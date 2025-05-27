using SK.Visit.Domain.Enum;
using System.Net.NetworkInformation;

namespace SK.Visit.Domain.Entities
{
    public class Destination : BaseEntity
    {
        public string AgentId { get; set; }
        public string? OrderId { get; set; }

        public string CustomerId { get; set; }

        public string AddressId { get; set; }

        public bool IsDelevery { get; set; } = false;

        public string? CompanyName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        
        public DateTime Date { get; set; }

        public string CityId { get; set; }

        public string StateId { get; set; }

        public string CountryId { get; set; }

        public string? ArrivalTime { get; set; }
        public string? FinishTime { get; set; }
        public string? Note { get; set; } = string.Empty;
        public string? SkipReason { get; set; } = string.Empty;

        public TripStatus Status { get; set; } = TripStatus.Pending;
    }
}
