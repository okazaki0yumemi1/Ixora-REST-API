using System.ComponentModel.DataAnnotations.Schema;

namespace Ixora_REST_API.Models
{
    public class OrderDetails : Entity
    {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; private set; }
            [ForeignKey("Goods")]
            public int GoodsId { get; set; } //Foreign key
            [ForeignKey("Order")]
            public int OrderId { get; private set; } //Foreign key
            public float ItemPrice { get; private set; }
            public int Count { get; set; }

        public void SetID(int id)
        {
            this.Id = id;
        }
    }
}
