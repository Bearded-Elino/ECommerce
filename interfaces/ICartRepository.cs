using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValeShop.Models;

namespace ValeShop.interfaces
{
    public interface ICartRepository
    {
        public Task AddToCart(Guid productId, int quantity);
        public Task RemoveFromCart(Guid productId);
        public Task<List<Cart>> GetCartItems();
        public Task ClearCart();
    }
}