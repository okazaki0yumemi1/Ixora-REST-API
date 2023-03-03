﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Ixora_REST_API.Models
{
    public class Goods
    {
        public Guid Id { get; set; }
        [ForeignKey("GroupID")]
        public GoodsType GoodsGroupID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        //public int LeftInStock { }
    }
}
