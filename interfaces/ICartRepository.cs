using System;
using System.Threading.Tasks;

namespace ValeShop.interfaces
{
    public interface ICartRepository
    {
        public void AddToCart(Guid productId, int quantity);
    }
}