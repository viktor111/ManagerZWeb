using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Data;
using WEBManagerZ.Models;

namespace WEBManagerZ.Services
{
    public class SqlProduct : ISqlProduct
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
            return _dbContexet.Products.OrderBy(p => p.Name).ToList();
        }
    }
}
