using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValeShop.interfaces;
using ValeShop.Repositories;
using ValeShop.ViewModels;

namespace ValeShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICountryRepository _countryRepository;

        public OrderController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IActionResult> Order()
        {
            var countries = await _countryRepository.GetAllCountriesAsync();

            var sortedCountries = countries.OrderBy(c => c.Name).ToList();


            var model = new CheckoutViewModel()
            {
                Countries = sortedCountries.Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),

            };

            return View(model);


        }
    }


}
