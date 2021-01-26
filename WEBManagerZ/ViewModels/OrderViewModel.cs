using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBManagerZ.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Notes { get; set; }
    }
}
