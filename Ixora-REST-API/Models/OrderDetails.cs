namespace Ixora_REST_API.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int GoodsId { get; set; } //Foreign key
        public Goods Goods { get; set; } //Reference
        //public int OrderId { get; set; } //Foreign key
        public Order Order { get; set; } //Reference
        public float ItemPrice { get; set; }
        public int Count { get; set; }

    }
}
