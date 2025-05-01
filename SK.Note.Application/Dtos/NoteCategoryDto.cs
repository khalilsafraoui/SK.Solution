using System.ComponentModel.DataAnnotations;

namespace SK.Note.Application.Dtos
{
    public class NoteCategoryDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Please entre name..")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please entre ordre..")]
        public int DisplayOrdre { get; set; } = 0;

        public string UserId { get; set; }
    }
}
