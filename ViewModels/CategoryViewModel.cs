using System;
using System.ComponentModel.DataAnnotations;

namespace ValeShop.ViewModels
{
    public class CategoryViewModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string CategoryName { get; set; }=String.Empty;
        public string CategoryDescription { get; set; }=String.Empty;
    }
}