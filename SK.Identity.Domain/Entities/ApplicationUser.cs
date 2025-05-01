using Microsoft.AspNetCore.Identity;

namespace SK.Identity.Domain.Entities
{

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
    }
}
