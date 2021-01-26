using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WEBManagerZ.Models;
using WEBManagerZ.Services;

namespace WEBManagerZ.Controllers
{
    public class ProductController : Controller
    {
        private SqlProduct _sqlProduct;
        private SqlCart _sqlCart;
        private UserManager<AppUser> _userManager;

        public ProductController(SqlProduct sqlProduct, 
            SqlCart sqlCart,
            UserManager<AppUser> userManager
            )
        {
            _sqlProduct = sqlProduct;
            _sqlCart = sqlCart;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllProducts()
        {
            List<Product> p = _sqlProduct.GetProducts();
            return View(p);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            AppUser user = await _userManager.GetUserAsync(User);
            Cart cart =_sqlCart.GetCart(user);
            Product product = _sqlProduct.GetProduct(id);

            _sqlCart.AddProductToCart(product, cart);

            return RedirectToAction(nameof(GetAllProducts));
        }
    }
}
