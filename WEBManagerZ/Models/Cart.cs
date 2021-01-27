using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBManagerZ.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int ProductCount { get; set; }

        public decimal Price { get; set; }

        public AppUser User { get; set; }

        public List<CartProduct> CartProduct { get; set; }
    }
}
