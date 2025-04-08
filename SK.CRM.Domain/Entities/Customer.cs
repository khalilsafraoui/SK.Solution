namespace SK.CRM.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
        }

        public Customer(string userId, string email,string firstName,string lastName)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            IsProspect = true;
        }
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string? UserId { get; set; }

        public bool IsProspect { get; set; } = false;

        public bool IsDisabled { get; set; } = false;

        public string DeliveryMethod { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = string.Empty;

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
