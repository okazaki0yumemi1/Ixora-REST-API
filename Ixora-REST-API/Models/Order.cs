using System.ComponentModel.DataAnnotations.Schema;

namespace Ixora_REST_API.Models
{
    public class Order : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; } //Foreign key
        //public Client Client { get; private set; } //Reference
        public DateTime CreationDate { get; private set; } = DateTime.Now;
        public bool IsComplete { get; private set; } = false;
        //public List<int> OrderDetailsId { get; set; } //Foreign key
        public IEnumerable<OrderDetails> OrderDetails { get; set; } //Reference
        public Order()
        {

        }
        public Order(int ClientId, bool OrderStatus)
        {
            ID = ClientId;
            IsComplete = OrderStatus;
        }
        //public void AddOrderDetails(IEnumerable<OrderDetails> first)
        //{
        //    this.OrderDetails = this.OrderDetails.Union(first);
        //}
        public void ChangeStatus(bool newStatus)
        {
            this.IsComplete = newStatus;
        }
    }
}
