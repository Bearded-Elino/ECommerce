using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValeShop.Models
{
    public class Promo
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }=String.Empty;
        public bool IsRunning { get; set; } = false;
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public DateTime CreatedDate { get; set; }=DateTime.Now;
    }
}