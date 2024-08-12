using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ValeShop.Data;
using ValeShop.Models;

namespace ValeShop.CountryService
{
    public class CountryServiceClass
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public CountryServiceClass(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task FetchAndInsertCountries()
        {
            var response = await _httpClient.GetStringAsync("https://restcountries.com/v3.1/all");
            var countries = JArray.Parse(response);
            var countryList = countries
                .Select(country => new Country
                {
                    Id = Guid.NewGuid(),
                    Name = country["name"]?["common"]?.ToString()
                })
                .Where(country => !string.IsNullOrWhiteSpace(country.Name) && !_context.Countries.Any(c => c.Name == country.Name))
                .ToList();

            if (countryList.Any())
            {
                _context.Countries.AddRange(countryList);
                await _context.SaveChangesAsync();
            }
        }
    }
}