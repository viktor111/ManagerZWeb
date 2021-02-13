using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Data;
using WEBManagerZ.Models;

namespace WEBManagerZ.Services
{
    public class SqlProduct
    {
        private ManagerZContext _dbContexet;

        public SqlProduct(ManagerZContext dbContext)
        {
            _dbContexet = dbContext;
        }

        public Product GetProduct(int id)
        {
            return _dbContexet.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts()
        {
            List<Product> ps = _dbContexet.Products.OrderBy(p => p.Name).ToList();

            List<Product> newPs = new();

            foreach (var p in ps)
            {
                if (p.DiscountId is not null)
                {
                    Discount discount = _dbContexet.Discount.Where(d => d.Id == p.DiscountId).FirstOrDefault();

                    decimal discountCalc = discount.Percent / 100;
                    decimal discountedPriceProduct = p.FinalPrice - (discountCalc * p.FinalPrice);

                    p.PriceDiscounted = discountedPriceProduct;
                }
                newPs.Add(p);
            }

            return newPs;
        }

        public Product UpdatePicture(Product productModel)
        {
            Product product = _dbContexet.Products.Where(p => p.Id == productModel.Id).FirstOrDefault();

            product.Picture = productModel.Picture;

            _dbContexet.SaveChanges();

            return new Product();
        }

        public Product UpdateDescription(Product productModel)
        {
            Product product = _dbContexet.Products.Where(p => p.Id == productModel.Id).FirstOrDefault();

            product.Description = productModel.Description;

            _dbContexet.SaveChanges();

            return new Product();
        }
    }
}
