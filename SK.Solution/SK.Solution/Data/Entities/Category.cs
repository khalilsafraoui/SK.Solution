using System.ComponentModel.DataAnnotations;

namespace SK.Solution.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please entre name..")]
        public string Name { get; set; }
    }
}
