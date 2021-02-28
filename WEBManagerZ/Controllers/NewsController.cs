using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using WEBManagerZ.Models;
using WEBManagerZ.Services;

namespace WEBManagerZ.Controllers
{
    public class NewsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private SqlProduct _sqlProduct;
        private SqlCart _sqlCart;
        private readonly SqlNews _sqlNews;
        private UserManager<AppUser> _userManager;

        public NewsController
            (
            SqlProduct sqlProdut, 
            SqlCart sqlCart, 
            SqlNews sqlNews,
            UserManager<AppUser> userManager, 
            IWebHostEnvironment webHostEnvironment
            )
        {
            _sqlProduct = sqlProdut;
            _sqlCart = sqlCart;
            _sqlNews = sqlNews;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(News news)
        {
            string uniqueFileName = UploadedFile(news);

            News newNews = new();

            newNews.Picture = uniqueFileName;
            newNews.Text = news.Text;
            newNews.Date = DateTime.Now;
            newNews.Link = newNews.Link;

            _sqlNews.Create(newNews);
            return RedirectToAction(nameof(ListNews));
        }

        public IActionResult ListNews()
        {
            List<News> news = _sqlNews.ListNews();

            return View(news);
        }
        
        public IActionResult DeleteOne(int id)
        {
            News news = _sqlNews.GetOne(id);
            _sqlNews.DeleteOne(news);

            return RedirectToAction(nameof(ListNews));
        }

        public string UploadedFile(News model)
        {
            string uniqueFileName = null;

            if (model.PictureFile != null)
            {
                string uploadsFolder = "wwwroot/images";
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PictureFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PictureFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
