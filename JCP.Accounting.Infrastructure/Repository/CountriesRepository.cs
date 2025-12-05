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

        public async Task<List<Country>> GetAllAsync()
        {
            List<Country> retValue = await context.Countries.OrderBy(c => c.CountryID).ToListAsync();
            return retValue;
        }

        public async Task<Country?> GetByIdAsync(int countryID)
        {
            Country? retValue = await context.Countries.Where(c => c.CountryID == countryID).FirstOrDefaultAsync();
            return retValue;
        }

        public async Task<List<Country>> GetByFilterAsync(string name)
        {
            List<Country> retValue = await context.Countries
                                               .Where(c => c.Name.Contains(name))
                                               .OrderBy(c => c.Name)
                                               .ToListAsync();
            return retValue;
        }

        public async Task<int> AddAsync(Country country)
        {
            try
            {
                context.Countries.Add(country);
                await context.SaveChangesAsync();
                return country.CountryID;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(Country country)
        {
            try
            {
                context.Countries.Update(country);
                return await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteAsync(int countryID)
        {
            try
            {
                return await context.Countries
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