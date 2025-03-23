namespace SK.CRM.Domain.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

    }
}
