using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Data;
using WEBManagerZ.Models;

namespace WEBManagerZ.Services
{
    public class SqlDiscount
    {
        private ManagerZContext _dbContexet;

        public SqlDiscount(ManagerZContext dbContext)
        {
            _dbContexet = dbContext;
        }

        public Discount CreateDiscount(Discount discount)
        {
            _dbContexet.Discount.Add(discount);

            _dbContexet.SaveChanges();

            return new Discount();
        }

        public List<Discount> ListDiscount()
        {
            return _dbContexet.Discount.OrderBy(a => a.StartDate).ToList();
        }

        public Discount GetDiscount(int id)
        {
            return _dbContexet.Discount.Where(d => d.Id == id).FirstOrDefault();
        }

        public Discount AddProductToDiscount(string dName, string pName)
        {
            Discount discount = _dbContexet.Discount.Where(d => d.Name == dName).FirstOrDefault();
            Product product = _dbContexet.Products.Where(p => p.Name == pName).FirstOrDefault();

            List<Product> ps = new List<Product>();

            ps.Add(product);
            
            if(discount.Products != null)
            {
                foreach (var p in discount.Products)
                {
                    ps.Add(p);
                }
            }

            discount.Products = ps;
            product.Discount = discount;

            _dbContexet.SaveChanges();

            return new Discount();
        }
    }
}
