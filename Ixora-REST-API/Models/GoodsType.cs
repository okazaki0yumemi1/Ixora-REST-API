using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ixora_REST_API.Models
{
    public class GoodsType : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }
        public string GroupName { get; set; }

        public GoodsType()
        {

        }
    }
}
