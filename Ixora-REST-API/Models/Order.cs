using System.ComponentModel.DataAnnotations.Schema;

namespace Ixora_REST_API.Models
{
    public class Order : Entity
    {
        public int ID { get; set; }
        public int ClientId { get; set; } //Foreign key
        public Client Client { get; set; } //Reference
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsComplete { get; set; } = false;
        //public List<int> OrderDetailsId { get; set; } //Foreign key
        public IEnumerable<OrderDetails> OrderDetails { get; set; } //Reference
        public Order()
        {

        }
    }
}
