using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValeShop.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string SessionId { get; set; }=String.Empty;
        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public DateTime CreatedDate { get; set; }=DateTime.Now;
    }
}