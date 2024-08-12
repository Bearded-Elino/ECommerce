using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValeShop.Models;

namespace ValeShop.interfaces
{
    public interface IOrderDetailsRepository
    {
        Task<List<OrderDetails>> GetOrderDetailsByOrderIdAsync(Guid orderId);

    }
}