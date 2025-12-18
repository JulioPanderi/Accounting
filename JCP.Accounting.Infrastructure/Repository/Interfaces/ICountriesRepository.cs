using Accounting.Domain.DTO;

namespace Accounting.Infrastructure.Interfaces
{
    public interface ICountriesRepository
    {
        Task<List<CountryDTO>> GetAllAsync();
        Task<List<CountryDTO>> GetByFilterAsync(string name);
        Task<CountryDTO?> GetByIdAsync(int countryID);
        Task<short> AddAsync(CountryDTO countryDTO);
        Task UpdateAsync(CountryDTO countryDTO);
        Task DeleteAsync(int countryID);
    }
}