using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBManagerZ.Models
{
    public class CartProduct
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
