using System.ComponentModel.DataAnnotations.Schema;

namespace Ixora_REST_API.Models
{
    public class Order : Entity
    {
        //public Guid ID { get; set; }
        public int ID { get; set; }
        public int ClientId { get; set; } //Foreign key
        public Client Client { get; set; } //Reference
        public DateTime CreationDate { get; private set; } = DateTime.Now;
        public bool IsComplete { get; set; } = false;
        public int OrderDetailsId { get; set; } //Foreign key
        public OrderDetails OrderDetails { get; set; } //Reference
    }
}
