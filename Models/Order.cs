using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ValeShop.Models.Enum;

namespace ValeShop.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }=DateTime.Now;


    }
}