namespace SK.Inventory.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
