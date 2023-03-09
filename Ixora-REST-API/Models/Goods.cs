using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ixora_REST_API.Models
{
    public class Goods : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        [ForeignKey("GoodsType")]
        public int GoodsTypeID { get; set; } //Foreign key
        //public GoodsType GoodsType { get; private set; } //Reference
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int LeftInStock { get; set; }
        public Goods()
        {

        }
    }
}
