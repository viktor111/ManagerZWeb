using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEBManagerZ.Models
{
    public class News
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Picture { get; set; }

        public string Link { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        [NotMapped]
        public IFormFile PictureFile { get; set; }
    }
}
