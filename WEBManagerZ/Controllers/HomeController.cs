using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Models;
using WEBManagerZ.Services;
using WEBManagerZ.ViewModels;

namespace WEBManagerZ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SqlCart _sqlCart;
        private UserManager<AppUser> _userManager;
        private SqlDiscount _sqlDiscount;
        private SqlNews _sqlNews;

        public HomeController(ILogger<HomeController> logger,
            SqlCart sqlCart,
            UserManager<AppUser> userManager,
            SqlNews sqlNews,
            SqlDiscount sqlDiscount
            )
        {
            _logger = logger;
            _userManager = userManager;
            _sqlNews = sqlNews;
            _sqlDiscount = sqlDiscount;
            _sqlCart = sqlCart;

        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel indexViewModel = new();
            indexViewModel.Discounts = _sqlDiscount.ListDiscount();
            indexViewModel.News = _sqlNews.ListNews();
            

            return View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
