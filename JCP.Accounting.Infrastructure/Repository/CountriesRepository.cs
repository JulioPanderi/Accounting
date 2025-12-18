using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repository
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly AccountingDbContext context;

        public CountriesRepository(AccountingDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<CountryDTO>> GetAllAsync()
        {
            List<Country> countries = await context.Countries.OrderBy(c => c.CountryID).ToListAsync();

            return (countries?.Select(c => Mappers.DtoMappers.MapCountryToDTO(c)).ToList());
        }

        public async Task<CountryDTO?> GetByIdAsync(int countryID)
        {
            Country? country = await context.Countries.Where(c => c.CountryID == countryID).FirstOrDefaultAsync();
            return (country == null ? null : Mappers.DtoMappers.MapCountryToDTO(country));
        }

        public async Task<List<CountryDTO>> GetByFilterAsync(string name)
        {
            List<Country> countries = await context.Countries
                                                   .Where(c => c.Name.Contains(name))
                                                   .OrderBy(c => c.Name)
                                                   .ToListAsync();
            return (countries?.Select(c => Mappers.DtoMappers.MapCountryToDTO(c)).ToList());
        }

        public async Task<short> AddAsync(CountryDTO countryDTO)
        {
            try
            {
                Country country = Mappers.EntityMappers.MapToCountry(countryDTO);
                context.Countries.Add(country);
                await context.SaveChangesAsync();
                return country.CountryID;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(CountryDTO countryDTO)
        {
            try
            {
                Country country = Mappers.EntityMappers.MapToCountry(countryDTO);
                context.Countries.Update(country);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(int countryID)
        {
            try
            {
                await context.Countries
                             .Where(c => c.CountryID == countryID)
                             .ExecuteDeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}