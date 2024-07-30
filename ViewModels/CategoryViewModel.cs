using System;
using System.ComponentModel.DataAnnotations;

namespace ValeShop.ViewModels
{
    public class CategoryViewModel
    {
        public Guid? ParentId { get; set; }
        [Required]
        public string CategoryName { get; set; }=String.Empty;
        public string CategoryDescription { get; set; }=String.Empty;
    }
}