using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Models;

namespace WEBManagerZ.Services
{
    public interface ISqlOrder
    {
        Order SaveOrder(Order order);
    }
}
