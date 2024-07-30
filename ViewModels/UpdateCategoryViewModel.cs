using System;

namespace ValeShop.ViewModels
{
    public class UpdateCategoryViewModel
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }=String.Empty;
        
        public string CategoryDescription { get; set; }=String.Empty;
    }
}