using System.Collections.Generic;
using System.Linq;
using WEBManagerZ.Data;
using WEBManagerZ.Models;

namespace WEBManagerZ.Services
{
    public class SqlProduct
    {
        private readonly ManagerZContext _dbContext;

        public SqlProduct(ManagerZContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product GetProduct(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts()
        {
            List<Product> ps = _dbContext.Products.OrderBy(p => p.Name).ToList();

            List<Product> newPs = new();

            foreach (var p in ps)
            {
                if (p.DiscountId is not null)
                {
                    Discount discount = _dbContext.Discount.FirstOrDefault(d => d.Id == p.DiscountId);

                    if (discount is not null)
                    {
                        decimal discountCalc = discount.Percent / 100;
                        decimal discountedPriceProduct = p.FinalPrice - (discountCalc * p.FinalPrice);

                        p.PriceDiscounted = discountedPriceProduct;
                    }
                }
                newPs.Add(p);
            }

            return newPs;
        }

        public Product UpdatePicture(Product productModel)
        {
            Product product = _dbContext.Products.FirstOrDefault(p => p.Id == productModel.Id);

            if (product is not null) product.Picture = productModel.Picture;

            _dbContext.SaveChanges();

            return new Product();
        }

        public Product UpdateDescription(Product productModel)
        {
            Product product = _dbContext.Products.FirstOrDefault(p => p.Id == productModel.Id);

            if (product is not null) product.Description = productModel.Description;

            _dbContext.SaveChanges();

            return new Product();
        }
    }
}
