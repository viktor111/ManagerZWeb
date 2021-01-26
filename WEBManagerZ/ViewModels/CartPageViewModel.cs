using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBManagerZ.ViewModels
{
    public class CartPageViewModel
    {
        public int CartQuantity { get; set; }

        public decimal CartPrice { get; set; }

        public List<CartItemViewModel> CartItemViewModels { get; set; }
    }
}
