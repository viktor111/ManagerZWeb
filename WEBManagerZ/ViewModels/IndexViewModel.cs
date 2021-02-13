using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Models;

namespace WEBManagerZ.ViewModels
{
    public class IndexViewModel
    {
        public List<News> News { get; set; }

        public List<Discount> Discounts { get; set; }
    }
}
