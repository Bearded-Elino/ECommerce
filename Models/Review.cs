using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ValeShop.Models
{
    [Index("Email")]
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }=String.Empty;
        public string Email { get; set; }=String.Empty;
        public string Content { get; set; }=String.Empty;
        public int Rating { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public DateTime CreatedDate = DateTime.Now;
    }
}