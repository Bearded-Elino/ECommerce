using System;
using System.ComponentModel.DataAnnotations;

namespace ValeShop.Models
{
    public class Country
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }=String.Empty;
    }
}