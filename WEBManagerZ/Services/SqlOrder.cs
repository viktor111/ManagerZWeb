using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Data;
using WEBManagerZ.Models;

namespace WEBManagerZ.Services
{
    public class SqlOrder
    {
        private ManagerZContext _dbContexet;

        public SqlOrder(ManagerZContext dbContext)
        {
            _dbContexet = dbContext;
        }

        public Order SaveOrder(Order order)
        {
            _dbContexet.Orders.Add(order);

            _dbContexet.SaveChanges();

            return new Order();
        }
    }
}
