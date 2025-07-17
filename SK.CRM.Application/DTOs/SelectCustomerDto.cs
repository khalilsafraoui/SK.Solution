using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Application.DTOs
{
    public class SelectCustomerDto
    {
        public Guid Id { get; set; }

        public string? UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
    }
}
