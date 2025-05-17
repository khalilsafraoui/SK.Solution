namespace SK.Solution.Shared.Model.Crm
{
    public class SharedCustomerDestinationDto
    {
        public Guid? CustomerId { get; set; }
        public Guid? AddressId { get; set; }

        public bool IsDelevery { get; set; } = false;
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        
        public SharedMarkDto? Mark { get; set; }

        public string CityId { get; set; }

        public string StateId { get; set; }

        public string CountryId { get; set; }
    }
}
