using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ValeShop.ViewModels
{
    public class BillingDetailsViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    
        [Required]
        public string CompanyName { get; set; } = string.Empty;
    
        [Required]
        public string Phone { get; set; } = string.Empty;
    
        [Required]
        public string Address { get; set; } = string.Empty;
    
        [Required]
        public string City { get; set; } = string.Empty;
    
        [Required]
        public Guid StateId { get; set; }

        [Required]
        public string ZipCode { get; set; } = string.Empty;
    
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    
        public bool IsActive { get; set; }
    
        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> States { get; set; } = new List<SelectListItem>();
        
        public Guid? SelectedCountryId { get; set; }

    }
}