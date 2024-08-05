using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Models;

namespace ValeShop.ViewModels
{
    public class ProductViewModel
    {
        
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public string Name { get; set; }=String.Empty;
        public string Description { get; set; }=String.Empty;
        public IFormFile? ImageFile { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }=String.Empty;
    }

}