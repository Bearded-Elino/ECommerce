using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValeShop.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public string ProductId { get; set; }=String.Empty;
        public string Description { get; set; }=String.Empty;
        public string ImageUrl { get; set; } =String.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedDate = DateTime.Now;
    }
}