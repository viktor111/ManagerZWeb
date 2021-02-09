﻿using System;
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
            return _dbContexet.Products.OrderBy(p => p.Name).ToList();
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
