using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBManagerZ.ViewModels
{
    public class ProductDescriptionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please add description")]
        public string Description { get; set; }
    }
}
