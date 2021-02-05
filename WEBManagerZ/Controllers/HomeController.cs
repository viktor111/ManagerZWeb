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

namespace WEBManagerZ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SqlCart _sqlCart;
        private UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger,
            SqlCart sqlCart,
            UserManager<AppUser> userManager
            )
        {
            _logger = logger;
            _userManager = userManager;
            _sqlCart = sqlCart;

        }

        public async Task<IActionResult> Index()
        { 
            return View();
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
