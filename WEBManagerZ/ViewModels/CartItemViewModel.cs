using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Models;

namespace WEBManagerZ.ViewModels
{
    public class CartItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal CostToMake { get; set; }

        public int Quantity { get; set; }

        public Discount Discount { get; set; }

        public decimal PriceDiscounted { get; set; } = 0;

        public bool DiscountExists { get; set; } = false;
    }
}
