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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Date for delivery")]
        public DateTime Date { get; set; }
        
        public string Notes { get; set; }
    }
}
