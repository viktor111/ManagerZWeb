using System;
using System.Collections.Generic;

#nullable disable

namespace WEBManagerZ.Models
{
    public partial class Order
    {
        public int Id { get; set; }

        public string Status { get; set; }  
        
        public DateTime Date { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public decimal Spent { get; set; }

        public int ProductCount { get; set; }

        public string ProductIds { get; set; }

        public string ProductNames { get; set; }

        public string Note { get; set; }
    }
}
