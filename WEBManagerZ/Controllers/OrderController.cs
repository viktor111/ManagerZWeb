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
    public class OrderController : Controller
    {
        private SqlProduct _sqlProduct;
        private SqlCart _sqlCart;
        private UserManager<AppUser> _userManager;
        private SqlOrder _sqlOrder;

        public OrderController(SqlProduct sqlProduct,
            SqlCart sqlCart,
            UserManager<AppUser> userManager,
            SqlOrder sqlOrder
            )
        {
            _sqlProduct = sqlProduct;
            _sqlCart = sqlCart;
            _userManager = userManager;
            _sqlOrder = sqlOrder;
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            Cart cart = _sqlCart.GetCart(user);

            List<CartItemViewModel> cartViewModels = _sqlCart.GetProductsVM(cart);

            decimal sumOfPrice = cartViewModels.Sum(p => p.Price);

            int sumOfQuantity = cartViewModels.Sum(p => p.Quantity);

            ViewData["ProuctSumPrice"] = sumOfPrice;
            ViewData["ProductCount"] = sumOfQuantity;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderViewModel orderViewModel)
        {
            AppUser user = await _userManager.GetUserAsync(User);
            Cart cart = _sqlCart.GetCart(user);

            List<CartItemViewModel> cartViewModels = _sqlCart.GetProductsVM(cart);
            List<int> ids = cartViewModels.Select(cvm => cvm.Id).ToList();
            List<string> namesAndQuantity = new List<string>();

            foreach (var p in cartViewModels)
            {
                string nameAndChar = p.Name + " - " + p.Quantity;

                namesAndQuantity.Add(nameAndChar);
            }

            decimal sumOfPrice = cartViewModels.Sum(p => p.Price);
            decimal sumOfSpent = cartViewModels.Sum(p => p.CostToMake);
            int sumOfQuantity = cartViewModels.Sum(p => p.Quantity);

            Order order = new Order();

            order.Name = orderViewModel.Name;
            order.PhoneNumber = orderViewModel.PhoneNumber;
            order.Address = orderViewModel.Adress;
            order.Note = orderViewModel.Notes;
            order.Status = "Sent";
            order.Date = orderViewModel.Date;
            order.Cost = cart.Price;
            order.ProductCount = sumOfQuantity;
            order.ProductIds = string.Join(",", ids);
            order.Spent = sumOfSpent;
            order.ProductNames = string.Join(", ", namesAndQuantity);

            _sqlOrder.SaveOrder(order);

            _sqlCart.ClearCart(cart);

            return RedirectToAction("Cart", "Cart");
        }
    }
}
