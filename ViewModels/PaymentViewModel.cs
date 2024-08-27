using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ValeShop.Models;

namespace ECommerce.ViewModels
{
    public class PaymentViewModel
    {

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The amount must be greater than zero.")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}