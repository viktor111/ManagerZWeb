﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Data;
using WEBManagerZ.Models;
using WEBManagerZ.ViewModels;

namespace WEBManagerZ.Services
{
    public class SqlCart : ISqlCart
    {
        private ManagerZContext _dbContexet;

        public SqlCart(ManagerZContext dbContext)
        {
            _dbContexet = dbContext;
        }


        public Cart AddProductToCart(Product product, Cart cart)
        {

            var productDb = _dbContexet.Products.Where(cp => cp.Id == product.Id).Include(x => x.CartProduct).FirstOrDefault();
            var cartProduct = productDb.CartProduct.Where(x => x.CartId == cart.Id).FirstOrDefault();

            if (cartProduct != null)
            {
                //var quantityOfCart = _dbContexet.CartProduct.Where(cp => cp.CartId == cart.Id).FirstOrDefault().Quantity;

                cartProduct.Quantity = cartProduct.Quantity + 1;
            }
            else
            {
                productDb.CartProduct.Add(new CartProduct { CartId = cart.Id });
            }

            //_dbContexet.Update(product) if you are using QueryTrackingBehavior.NoTracking
            _dbContexet.SaveChanges();
            return cart;
        }

        public Cart CreateCart(AppUser user)
        {
            Cart cart = new Cart();
            cart.User = user;
            _dbContexet.Carts.Add(cart);
            return new Cart();
        }

        public Cart GetCart(AppUser user)
        {
            Cart cart = _dbContexet.Carts.Where(c => c.User == user).FirstOrDefault();

            return cart;
        }

        public List<Product> Products(Cart cart)
        {
            return _dbContexet.Carts.Where(p => p.Id == cart.Id).SelectMany(p => p.CartProduct).Select(p => p.Product).ToList();
        }

        public List<CartItemViewModel> GetProductsVM(Cart cart)
        {
            List<CartItemViewModel> viewModels = new List<CartItemViewModel>();

            var productDb = _dbContexet.CartProduct.Where(cp => cp.CartId == cart.Id).ToList();

            foreach(var smth in productDb)
            {
                CartItemViewModel cvm = new CartItemViewModel();
                int pId = smth.ProductId;
                int quantity = smth.Quantity;

                Product product = _dbContexet.Products.Where(p => p.Id == pId).FirstOrDefault();

                cvm.Price = product.FinalPrice;
                cvm.Quantity = quantity;
                cvm.Name = product.Name;
                cvm.Id = product.Id;

                viewModels.Add(cvm);
            }
            return viewModels;
        }

        public CartProduct DeleteProductFromCart(int productId)
        {
            CartProduct p = _dbContexet.CartProduct.Where(p => p.ProductId == productId).FirstOrDefault();

            if(p.Quantity == 1)
            {
                _dbContexet.CartProduct.Remove(p);

                p.Quantity = p.Quantity - 1;
            }
            else if(p.Quantity > 1)
            {
                p.Quantity = p.Quantity - 1;
            }
            _dbContexet.SaveChanges();

            return p;
        }
    }
}