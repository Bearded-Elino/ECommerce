using System;
using System.ComponentModel.DataAnnotations;

namespace ValeShop.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }=String.Empty;
        [Required]
        public string Password { get; set; }=String.Empty;
    }
}