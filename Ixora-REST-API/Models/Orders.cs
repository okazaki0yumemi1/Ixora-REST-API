namespace Ixora_REST_API.Models
{
    public class Orders
    {
        public Guid OrderID { get; set; }
        public int ClientID { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
