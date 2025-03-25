namespace SK.CRM.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
        }

        public Customer(string userId, string email)
        {
            UserId = userId;
            Email = email;
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
    }
}
