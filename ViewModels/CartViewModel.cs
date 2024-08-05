using System;
using System.Collections.Generic;
using ValeShop.Models;

namespace ValeShop.ViewModels
{
    public class CartViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        
        
    }
}