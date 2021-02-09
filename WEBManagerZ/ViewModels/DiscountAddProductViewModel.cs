using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Models;

namespace WEBManagerZ.ViewModels
{
    public class DiscountAddProductViewModel
    {
        public string ProductName { get; set; }

        public string DiscountName { get; set; }

        public SelectList ProductsName { get; set; }

        public SelectList DiscountsName { get; set; }
    }
}
