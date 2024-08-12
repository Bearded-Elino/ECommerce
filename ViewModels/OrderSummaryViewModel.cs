using System.Collections.Generic;

namespace ValeShop.ViewModels
{
    public class OrderSummaryViewModel
    {
        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
        public decimal OrderTotal { get; set; }
    }
}