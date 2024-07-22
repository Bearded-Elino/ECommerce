using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValeShop.Models
{
    public class State
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }=String.Empty;
        public string ZipCode { get; set; }=String.Empty;
        public Guid CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country? Country { get; set; }
    }
}