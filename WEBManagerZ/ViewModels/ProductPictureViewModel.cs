using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBManagerZ.ViewModels
{
    public class ProductPictureViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please choose image")]        
        public IFormFile Picture { get; set; }
    }
}
