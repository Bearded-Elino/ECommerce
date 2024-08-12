using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValeShop.Data;
using ValeShop.Models;

namespace ValeShop.CountryService
{
    public class CountryDataSeeder
    {
        private readonly AppDbContext _context;
        private readonly CountryServiceClass _countryService;

        public CountryDataSeeder(AppDbContext context, CountryServiceClass countryService)
        {
            _context = context;
            _countryService = countryService;
        }

        public async Task SeedAsync()
        {
            if (!_context.Countries.Any())
            {
                await _countryService.FetchAndInsertCountries();
            }
        }
    }

}