using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ValeShop.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string CategoryName { get; set; }=String.Empty;
        public string CategoryDescription { get; set; }=String.Empty;
        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Category? Parent { get; set; }
        
        public List<Category>? SubCategories { get; set; }
    }
}