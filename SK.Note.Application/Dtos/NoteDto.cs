using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SK.Note.Application.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please entre Title..")]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? DescriptionEn { get; set; }

        [Required(ErrorMessage = "Please entre ordre..")]
        public int DisplayOrdre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category.")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public NoteCategoryDto NoteCategory { get; set; }

        public string UserId { get; set; }
    }
}
