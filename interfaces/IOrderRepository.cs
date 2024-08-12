using System;
using System.Threading.Tasks;
using Mysqlx.Crud;
using Order = ValeShop.Models.Order;

namespace ValeShop.interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(Guid orderId);
    }
}