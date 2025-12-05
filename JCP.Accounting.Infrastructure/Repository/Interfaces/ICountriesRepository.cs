using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Interfaces
{
    public interface ICountriesRepository
    {
        Task<int> AddAsync(Country country);
        Task<int> DeleteAsync(int countryID);
        Task<List<Country>> GetAllAsync();
        Task<List<Country>> GetByFilterAsync(string name);
        Task<Country?> GetByIdAsync(int countryID);
        Task<int> UpdateAsync(Country country);
    }
}