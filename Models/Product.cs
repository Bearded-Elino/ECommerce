using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ValeShop.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; }=String.Empty;
        public string ImageUrl { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedDate = DateTime.Now;
    }
}