using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK.Note.Domain.Entities
{
    public class Note : BaseEntity
    {

        [Required(ErrorMessage = "Please entre Title..")]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? DescriptionEn { get; set; }

        [Required(ErrorMessage = "Please entre ordre..")]
        public int DisplayOrdre { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public NoteCategory NoteCategory { get; set; }

        public string UserId { get; set; }
    }
}
