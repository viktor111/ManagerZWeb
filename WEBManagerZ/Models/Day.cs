using System;
using System.Collections.Generic;

#nullable disable

namespace WEBManagerZ.Models
{
    public partial class Day
    {
        public int Id { get; set; }
        public decimal TotalMade { get; set; }
        public decimal TotalSpent { get; set; }
        public int SoldProductsCount { get; set; }
        public string MostCommonProduct { get; set; }
        public string MostCommonCategory { get; set; }
        public DateTime Date { get; set; }
    }
}
