using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.Controllers
{
    public class BillingDetailsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;

        public BillingDetailsController(AppDbContext context, ICountryRepository countryRepository, IStateRepository stateRepository)
        {
            _context = context;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
        }

        public async Task<IActionResult> BillingDetails(Guid? selectedCountryId)
        {
            var session = HttpContext.Session.GetString("userId");
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }

            // var countries = await _countryRepository.GetAllCountriesAsync();
            // var sortedCountries = countries.OrderBy(c => c.Name).ToList();

            // var model = new BillingDetailsViewModel
            // {
            //     Countries = sortedCountries.Select(c => new SelectListItem
            //     {
            //         Value = c.Id.ToString(),
            //         Text = c.Name
            //     }).ToList(),
            //     SelectedCountryId = selectedCountryId ?? Guid.Empty
            // };

            // if (selectedCountryId.HasValue)
            // {
            //     var states = await _stateRepository.GetStatesByCountryAsync(selectedCountryId.Value);
            //     model.States = states.Select(s => new SelectListItem
            //     {
            //         Value = s.Id.ToString(),
            //         Text = s.Name
            //     }).ToList();
            // }
            var model = new BillingDetailsViewModel();


            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> SaveBillingDetails(BillingDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {

                // var countries = await _countryRepository.GetAllCountriesAsync();
                // model.Countries = countries.Select(c => new SelectListItem
                // {
                //     Value = c.Id.ToString(),
                //     Text = c.Name
                // }).ToList();
                // Console.WriteLine("The code got to this point");

                return View("BillingDetails");
            }

            var billingDetails = new BillingDetails
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(HttpContext.Session.GetString("userId")),
                CompanyName = model.CompanyName,
                Phone = model.Phone,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                State = model.State,
                // StateId = model.StateId,
                IsActive = true
            };

            await _context.BillingDetails.AddAsync(billingDetails);
            await _context.SaveChangesAsync();
            Console.WriteLine($"{billingDetails}");

            TempData["BillingDetailsSuccess"] = "Order placed successfully";
            return RedirectToAction("Pay", "Payment");
        }


    }
}