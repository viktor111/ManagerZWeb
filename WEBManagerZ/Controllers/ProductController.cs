using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WEBManagerZ.Models;
using WEBManagerZ.Services;
using WEBManagerZ.ViewModels;

namespace WEBManagerZ.Controllers
{
    public class ProductController : Controller
    {
        private readonly SqlProduct _sqlProduct;
        private readonly SqlCart _sqlCart;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(SqlProduct sqlProduct, 
            SqlCart sqlCart,
            UserManager<AppUser> userManager,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _sqlProduct = sqlProduct;
            _sqlCart = sqlCart;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddDescription(ProductDescriptionViewModel model)
        {
            Product product = new Product();

            product.Id = model.Id;
            product.Description = model.Description;

            _sqlProduct.UpdateDescription(product);

            return RedirectToAction(nameof(GetAllProducts));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddDescription(int id)
        {
            ProductDescriptionViewModel model = new ProductDescriptionViewModel();
            model.Description = _sqlProduct.GetProduct(id).Description;

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddPicture(int id)
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddPicture(ProductPictureViewModel model)
        {
            string uniqueFileName = UploadedFile(model);

            Product product = new Product();

            product.Id = model.Id;
            product.Picture = uniqueFileName;

            _sqlProduct.UpdatePicture(product);

            return RedirectToAction(nameof(GetAllProducts));
        }

        public string UploadedFile(ProductPictureViewModel model)
        {
            string uniqueFileName = null;

            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
