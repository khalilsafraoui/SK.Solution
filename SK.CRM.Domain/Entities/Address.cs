namespace SK.CRM.Domain.Entities
{
    public class Address : BaseEntity
    {
        
        public string Street { get; set; }

        public string CityId { get; set; }

        public string StateId { get; set; }

        public string ZipCode { get; set; }

        public string CountryId { get; set; }

        public string? Name { get; set; }

        public string FullAddress { get; set; }

        // Foreign key to Customer
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public bool IsDeliveryAddress { get; set; } = false;

        public bool IsBillingAddress { get; set; } = false;

        // Flag to identify the default address
        public bool IsDefault { get; set; } = false;

        //Optional fields
        public decimal Latitude { get; set; }   // precision set via Fluent API
        public decimal Longitude { get; set; }  // precision set via Fluent API
    }

}
