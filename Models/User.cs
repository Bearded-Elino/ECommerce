using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ValeShop.Models
{
    [Index("Email")]
    [Index("Username")]
    public class User
    {
        
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }=String.Empty;
        public string LastName { get; set; }=String.Empty;
        public string Email { get; set; }=String.Empty;
        public string Username { get; set; }=String.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; }=String.Empty;
        [Required]
        [Compare("Password",ErrorMessage = "Passwords do not match.Try again")]
        public string ConfirmPassword { get; set; }=String.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}