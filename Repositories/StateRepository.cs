using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Models;

namespace ValeShop.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly AppDbContext _context;

        public StateRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<State>> GetAllCountriesAsync()
        {
            try
            {
                var allStates = await _context.States.ToListAsync();
                return allStates;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<State>> GetStatesByCountryAsync(Guid countryId)
        {
            return await _context.States
                .Where(s => s.CountryId == countryId)
                .ToListAsync();
        }
    }
}