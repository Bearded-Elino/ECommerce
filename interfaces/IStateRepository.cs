using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValeShop.Models;

namespace ValeShop.interfaces
{
    public interface IStateRepository
    {
        Task<List<State>> GetAllCountriesAsync();
        Task<List<State>> GetStatesByCountryAsync(Guid countryId);

    }
}