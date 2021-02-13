using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Picture { get; set; }
        public string Description { get; set; }
        public int TimesSold { get; set; }
        public int AddedToCart { get; set; }

        public List<CartProduct> CartProduct { get; set; }
        public Discount Discount { get; set; }
        public int? DiscountId { get; set; }
        [NotMapped]
        public decimal PriceDiscounted { get; set; }
        [NotMapped]
        public bool DiscountExist { get; set; }
    }
}
