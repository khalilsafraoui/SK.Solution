using System.ComponentModel.DataAnnotations;

namespace SK.Identity.Application.Dtos
{
    public class ManagerCreationDto
    {
        [Required(ErrorMessage = "Please entre FirstName..")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please entre LastName..")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please entre Email..")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please entre Password..")]
        public string Password { get; set; } = string.Empty;

        public Dictionary<string, bool> Roles { get; set; } = new();
    }
}
