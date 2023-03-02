namespace Ixora_REST_API.Models
{
    public class OrderDetails
    {
        public int GoodsID { get; set; }
        public int OrderID { get; set; }
        public float ItemPrice { get; set; }
        public int Count { get; set; }

    }
}
