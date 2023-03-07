using System.ComponentModel.DataAnnotations.Schema;

namespace Ixora_REST_API.Models
{
    public class Goods : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public int? GoodsTypeID { get; set; } //Foreign key
        //public GoodsType GoodsType { get; private set; } //Reference
        public string Name { get; set; }
        public float Price { get; set; }
        public int LeftInStock { get; set; }
        public Goods()
        {

        }
    }
}
