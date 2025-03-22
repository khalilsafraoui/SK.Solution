using System.ComponentModel.DataAnnotations;

namespace SK.Inventory.Domain.Entities.Product
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage ="Please entre name..")]
        public string Name { get; set; }
    }
}
