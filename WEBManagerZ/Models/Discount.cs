using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBManagerZ.Models
{
    public class Discount
    {
        public int Id { get; set; }

        public decimal Percent { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Product> Products { get; set; }
    }
}
