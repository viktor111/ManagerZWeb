using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Data;
using WEBManagerZ.Models;
using WEBManagerZ.ViewModels;

namespace WEBManagerZ.Services
{
    public class SqlCart
    {
        private readonly ManagerZContext _dbContext;

        public SqlCart(ManagerZContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Cart AddProductToCart(Product product, Cart cart)
        {

            var productDb = _dbContext.Products.Where(cp => cp.Id == product.Id).Include(x => x.CartProduct).FirstOrDefault();
            if (productDb != null)
            {
                var cartProduct = productDb.CartProduct.FirstOrDefault(x => x.CartId == cart.Id);

                if (cartProduct != null)
                {
                    cartProduct.Quantity = cartProduct.Quantity + 1;
                }
                else
                {
                    productDb.CartProduct.Add(new CartProduct { CartId = cart.Id });
                }
            }

            var dbCart = _dbContext.Carts.FirstOrDefault(c => c.Id == cart.Id);

            if (product.DiscountId is not null)
            {
                Discount discount = _dbContext.Discount.FirstOrDefault(d => d.Id == product.DiscountId);

                decimal discountCalc = discount.Percent / 100;
                decimal discountedPriceProduct = product.FinalPrice - (discountCalc * product.FinalPrice);

                if (dbCart is not null) dbCart.Price += discountedPriceProduct;
            }
            else
            {
                if (dbCart is not null) dbCart.Price += product.FinalPrice;
            }

            product.AddedToCart++;
           
            _dbContext.SaveChanges();
            return cart;
        }

        public Cart CreateCart(AppUser user)
        {
            Cart cart = new();
            cart.User = user;

            _dbContext.Carts.Add(cart);

            return new Cart();
        }

        public Cart GetCart(AppUser user)
        {
            Cart cart = _dbContext.Carts.FirstOrDefault(c => c.User == user);
            if(user != null)
            {
                if (cart.Price < 0)
                {
                    cart.Price = 0;
                }
            }            

            _dbContext.SaveChanges();

            return cart;
        }

        public List<Product> Products(Cart cart)
        {
            return _dbContext.Carts.Where(p => p.Id == cart.Id).SelectMany(p => p.CartProduct).Select(p => p.Product).ToList();
        }

        public Cart ClearCart(Cart cart)
        {
            var cartProduct = _dbContext.CartProduct.Where(x => x.CartId == cart.Id).ToList();
            var cartSql = _dbContext.Carts.FirstOrDefault(c => c.Id == cart.Id);

            cartSql.Price = 0;

            _dbContext.CartProduct.RemoveRange(cartProduct);

            _dbContext.SaveChanges();

            return new Cart();
        }

        public List<CartItemViewModel> GetProductsVM(Cart cart)
        {
            List<CartItemViewModel> viewModels = new List<CartItemViewModel>();

            var productDb = _dbContext.CartProduct.Where(cp => cp.CartId == cart.Id).ToList();

            foreach(var smth in productDb)
            {
                CartItemViewModel cvm = new CartItemViewModel();
                int pId = smth.ProductId;
                int quantity = smth.Quantity;

                Product product = _dbContext.Products.FirstOrDefault(p => p.Id == pId);

                cvm.Price = product.FinalPrice;
                cvm.Quantity = quantity;
                cvm.Name = product.Name;
                cvm.CostToMake = product.CostToMake;
                cvm.Id = product.Id;
                if(product.DiscountId is not null)
                {
                    Discount discount = _dbContext.Discount.FirstOrDefault(d => d.Id == product.DiscountId);
                    cvm.Discount = discount;
                    decimal discountCalc = discount.Percent / 100;

                    cvm.DiscountExists = true;
                    cvm.PriceDiscounted = product.FinalPrice - (discountCalc * product.FinalPrice);
                }

                viewModels.Add(cvm);
            }
            return viewModels;
        }

        public CartProduct AddQuantity(Product product, Cart cart)
        {
            CartProduct p = _dbContext.CartProduct.FirstOrDefault(p => p.ProductId == product.Id);

            p.Quantity += 1;
            
            var dbCart = _dbContext.Carts.FirstOrDefault(c => c.Id == cart.Id);

            if (product.DiscountId is not null)
            {
                Discount discount = _dbContext.Discount.FirstOrDefault(d => d.Id == product.DiscountId);

                decimal discountCalc = discount.Percent / 100;
                decimal discountedPriceProduct = product.FinalPrice - (discountCalc * product.FinalPrice);

                dbCart.Price += discountedPriceProduct;
            }
            else
            {
                dbCart.Price += product.FinalPrice;
            }

            _dbContext.SaveChanges();

            return p;
        }

        public CartProduct DeleteProductFromCart(Product product, Cart cart)
        {
            CartProduct p = _dbContext.CartProduct.FirstOrDefault(p => p.ProductId == product.Id);

            if(p.Quantity == 1)
            {
                _dbContext.CartProduct.Remove(p);

                p.Quantity = p.Quantity - 1;
            }
            else if(p.Quantity > 1)
            {
                p.Quantity = p.Quantity - 1;
            }

            var dbCart = _dbContext.Carts.FirstOrDefault(c => c.Id == cart.Id);

            if (product.DiscountId is not null)
            {
                Discount discount = _dbContext.Discount.FirstOrDefault(d => d.Id == product.DiscountId);

                decimal discountCalc = discount.Percent / 100;
                decimal discountedPriceProduct = product.FinalPrice - (discountCalc * product.FinalPrice);

                dbCart.Price -= discountedPriceProduct;
            }
            else
            {
                dbCart.Price -= product.FinalPrice;
            }

            _dbContext.SaveChanges();

            return p;
        }
    }
}
