using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ValeShop.ViewModels
{
    public class OrderViewModel
    {
        
            public Guid SelectedCountryId { get; set; }
            public List<SelectListItem> Countries { get; set; }
        
            public List<OrderItemViewModel> Items { get; set; }
    }
}