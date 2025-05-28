namespace SK.CRM.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}

