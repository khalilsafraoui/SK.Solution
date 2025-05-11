using System.ComponentModel.DataAnnotations;

namespace SK.Inventory.Domain.Entities.Product
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
    }
}
