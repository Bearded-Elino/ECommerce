using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValeShop.Models
{
    public class ProductPromo
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PromoId { get; set; }
        [ForeignKey(("PromoId"))]
        public Promo? Promo { get; set; }
        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
    }
}