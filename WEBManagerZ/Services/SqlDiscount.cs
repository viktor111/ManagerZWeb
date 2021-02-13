﻿using System;
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
        private SqlCart _sqlCart;

        public SqlDiscount(ManagerZContext dbContext, SqlCart sqlCart)
        {
            _dbContexet = dbContext;
            _sqlCart = sqlCart;
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

        public Discount DeleteOne(int id, Cart cart)
        {

            _sqlCart.ClearCart(cart);
                
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

            List<Product> ps = _dbContexet.Products.Where(p => p.DiscountId == id).ToList();

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

        public Discount AddProductToDiscount(string dName, string pName)
        {
            Discount discount = _dbContexet.Discount.Where(d => d.Name == dName).FirstOrDefault();

            if (discount is null)
            {
                return new Discount();
            }

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
            decimal discountCalc = discount.Percent / 100;
            product.PriceDiscounted = product.FinalPrice - (discountCalc * product.FinalPrice);

            _dbContexet.SaveChanges();

            return new Discount();
        }
    }
}
