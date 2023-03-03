using System.ComponentModel.DataAnnotations.Schema;

namespace Ixora_REST_API.Models
{
    public class Order
    {
        public Guid OrderID { get; set; }
        [ForeignKey("ClientID")]
        public int ClientID { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
