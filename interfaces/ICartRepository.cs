using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValeShop.Models;

namespace ValeShop.interfaces
{
    public interface ICartRepository
    {
        Task AddToCart(Guid productId, int quantity);
        Task RemoveFromCart(Guid productId);
        Task<List<Cart>> GetCartItems();
        Task ClearCart();
    }
}