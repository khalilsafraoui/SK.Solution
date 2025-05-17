namespace SK.Solution.Shared.Model.Crm
{
    public class SharedCustomerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string? UserId { get; set; }

        public bool IsProspect { get; set; } = false;

        public bool IsDisabled { get; set; } = false;

        public ICollection<SharedAddressDto> Addresses { get; set; } = new List<SharedAddressDto>();
    }
}
