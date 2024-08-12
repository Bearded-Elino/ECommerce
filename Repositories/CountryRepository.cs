using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Models;

namespace ValeShop.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _context;

        public CountryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryByIdAsync(Guid id)
        {
            try
            {
                var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
                if (country == null)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("unable to get country", e);
            }

            return null;
        }
    }
}