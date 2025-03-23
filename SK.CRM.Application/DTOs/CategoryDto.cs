

using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Application.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please entre name..")]
        public string Name { get; set; }
    }
}
