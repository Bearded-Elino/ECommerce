using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValeShop.Models;

namespace ValeShop.interfaces
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryByIdAsync(Guid id);
    }
}