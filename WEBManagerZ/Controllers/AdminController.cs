using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Models;
using WEBManagerZ.Services;
using WEBManagerZ.ViewModels;

namespace WEBManagerZ.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private SqlProduct _sqlProduct;
        private SqlCart _sqlCart;
        public SqlDiscount _sqlDiscount;
        private UserManager<AppUser> _userManager;

        public AdminController(
            SqlProduct sqlProduct,
            SqlCart sqlCart,
            SqlDiscount sqlDiscount,
            UserManager<AppUser> userManager
            )
        {
            _sqlProduct = sqlProduct;
            _sqlCart = sqlCart;
            _sqlDiscount = sqlDiscount;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateDiscount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateDiscount(Discount discount)
        {
            _sqlDiscount.CreateDiscount(discount);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ListDiscounts()
        {
            List<Discount> discounts = _sqlDiscount.ListDiscount();

            return View(discounts);
        }

        [HttpGet]
        public IActionResult AddProductToDiscount()
        {
            var vm = new DiscountAddProductViewModel();
            vm.DiscountsName = new SelectList(_sqlDiscount.ListDiscount().Select(d => d.Name));
            vm.ProductsName = new SelectList(_sqlProduct.GetProducts().Select(d => d.Name));

            return View(vm);
        }
        [HttpPost]
        public IActionResult AddProductToDiscount(DiscountAddProductViewModel viewModel)
        {
            _sqlDiscount.AddProductToDiscount(viewModel.DiscountName, viewModel.ProductName);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DeleteDiscount(int id)
        {
            _sqlDiscount.DeleteOne(id);

            return RedirectToAction(nameof(ListDiscounts));
        }

        [HttpGet]
        public IActionResult EditDiscount(int id)
        {
            Discount currentDiscount = _sqlDiscount.GetDiscount(id);

            return View(currentDiscount);
        }
        [HttpPost]
        public IActionResult EditDiscount(Discount model)
        {
            _sqlDiscount.EditDiscount(model);

            return RedirectToAction(nameof(ListDiscounts));
        }

        [HttpGet]
        public IActionResult DiscountProducts(int id)
        {
            List<Product> ps = _sqlDiscount.DiscountProducts(id);

            return View(ps);
        }
    }
}
