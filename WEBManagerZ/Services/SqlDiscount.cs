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

        public Discount DeleteOne(int id)
        {
            Discount d = _dbContexet.Discount.Where(d => d.Id == id).FirstOrDefault();

            _dbContexet.Discount.Remove(d);

            List<Product> ps = _dbContexet.Products.Where(p => p.DiscountId == id).ToList();

            foreach (var p in ps)
            {
                p.DiscountId = null;
            }

            _dbContexet.SaveChanges();

            return new Discount();
        }

        public Discount EditDiscount(Discount newDiscount)
        {
            Discount discount = _dbContexet.Discount.Where(d => d.Id == newDiscount.Id).FirstOrDefault();

            discount.Name = newDiscount.Name;
            discount.Percent = newDiscount.Percent;
            discount.Products = newDiscount.Products;
            discount.StartDate = newDiscount.StartDate;
            discount.EndDate = newDiscount.EndDate;
            discount.Description = newDiscount.Description;

            _dbContexet.SaveChanges();

            return discount;
        }

        public List<Product> DiscountProducts(int id)
        {
            return  _dbContexet.Products.Where(p => p.DiscountId == id).ToList();            
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
