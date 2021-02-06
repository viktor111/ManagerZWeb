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
    }
}
