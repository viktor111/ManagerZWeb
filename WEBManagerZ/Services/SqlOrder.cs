using System.Collections.Generic;
using WEBManagerZ.Data;
using WEBManagerZ.Models;

namespace WEBManagerZ.Services
{
    public class SqlOrder
    {
        private readonly ManagerZContext _dbContexet;
        private readonly SqlCart _sqlCart;

        public SqlOrder(ManagerZContext dbContext, SqlCart sqlCart)
        {
            _dbContexet = dbContext;
            _sqlCart = sqlCart;
        }

        public Order SaveOrder(Order order, Cart cart)
        {

            _dbContexet.Orders.Add(order);
            List<Product> products = _sqlCart.Products(cart);

            foreach (var p in products)
            {
                p.TimesSold++;
            }

            _dbContexet.SaveChanges();

            return new Order();
        }
    }
}
