using System;
using System.Collections.Generic;

#nullable disable

namespace WEBManagerZ.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public decimal CostToMake { get; set; }
        public decimal FinalPrice { get; set; }
        public int? OrderId { get; set; }
        public string Picture { get; set; }

        public virtual Order Order { get; set; }
        public List<CartProduct> CartProduct { get; set; }
    }
}
