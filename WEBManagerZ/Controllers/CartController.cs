using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Models;
using WEBManagerZ.Services;
using WEBManagerZ.ViewModels;

namespace WEBManagerZ.Controllers
{
    public class CartController : Controller
    {

        private SqlProduct _sqlProduct;
        private SqlCart _sqlCart;
        private UserManager<AppUser> _userManager;

        public CartController(SqlProduct sqlProduct,
            SqlCart sqlCart,
            UserManager<AppUser> userManager
            )
        {
            _sqlProduct = sqlProduct;
            _sqlCart = sqlCart;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
         { 
            AppUser user = await _userManager.GetUserAsync(User);
            Cart cart = _sqlCart.GetCart(user);

            CartPageViewModel cartPageViewModel = new CartPageViewModel();

            List<CartItemViewModel> cartViewModels = _sqlCart.GetProductsVM(cart);

            cartPageViewModel.CartItemViewModels = cartViewModels;

            //decimal sumOfPrice = cartViewModels.Sum(p => p.Price);

            int sumOfQuantity = cartViewModels.Sum(p => p.Quantity);

            cartPageViewModel.CartPrice = cart.Price;

            cartPageViewModel.CartQuantity = sumOfQuantity;
            
            CartItemViewModel model = new CartItemViewModel();

            return View(cartPageViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteItem(int id)
        {
            AppUser user = await _userManager.GetUserAsync(User);
            Cart cart = _sqlCart.GetCart(user);

            var product =_sqlProduct.GetProduct(id);
            _sqlCart.DeleteProductFromCart(product, cart);

            return RedirectToAction(nameof(cart));
        }
    }
}
