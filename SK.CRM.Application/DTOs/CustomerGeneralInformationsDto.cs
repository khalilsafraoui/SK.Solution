using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Application.DTOs
{
    public record CustomerGeneralInformationsDto
    {
        public Guid Id { get; set; }
        [Required]
        public  string FirstName { get; set; } = string.Empty;
        [Required]
        public  string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public  string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        public  string PhoneNumber { get; set; } = string.Empty;
    }
}
