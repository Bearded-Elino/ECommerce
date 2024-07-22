using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValeShop.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string CategoryName { get; set; }=String.Empty;
        public string CategoryDescription { get; set; }=String.Empty;
        public Guid ParentId { get; set; }
        public int SubCategory { get; set; }
        [ForeignKey("ParentId")]
        public Category? Parent { get; set; }
    }
}