using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValeShop.Models
{
    public class BillingDetails
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public  User? User { get; set; }
        public string CompanyName { get; set; }=String.Empty;
        public string Phone { get; set; }=String.Empty;
        public string Address { get; set; }=String.Empty;
        public string City { get; set; }=String.Empty;
        public int StateId { get; set; }
        public State State { get; set; }
        public bool IsActive { get; set; }
    }
}