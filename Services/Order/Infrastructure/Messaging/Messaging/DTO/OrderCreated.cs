namespace Messaging.DTO
{
    public class OrderCreated
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
