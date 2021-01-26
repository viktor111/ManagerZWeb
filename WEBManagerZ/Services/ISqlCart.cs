using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Models;

namespace WEBManagerZ.Services
{
    public interface ISqlCart
    {
        Cart AddProductToCart(Product product, Cart cart);
        Cart GetCart(AppUser user);
        Cart CreateCart(AppUser user);


    }
}
