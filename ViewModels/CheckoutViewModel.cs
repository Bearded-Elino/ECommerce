using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValeShop.Models;

namespace ValeShop.ViewModels
{
    public class CheckoutViewModel
    {
        public Guid SelectedCountryId { get; set; }
        public List<SelectListItem> Countries { get; set; }
        
        public List<OrderItemViewModel> Items { get; set; }

    }
}