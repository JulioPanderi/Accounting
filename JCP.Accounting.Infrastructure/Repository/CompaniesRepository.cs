using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repository
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly AccountingDbContext context;

        public CompaniesRepository(AccountingDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Company>> GetAllAsync()
        {
            List<Company> retValue = await context.Companies
                                           .OrderBy(c => c.TradeName)
                                           .Include(c => c.Country)
                                           .ToListAsync();
            return retValue;
        }

        public async Task<Company?> GetByIdAsync(int companyID)
        {
            Company? retValue = await context.Companies
                                      .Where(c => c.CompanyID == companyID)
                                      .Include(c => c.Country)
                                      .FirstOrDefaultAsync();
            return retValue;
        }

        public async Task<List<Company>> GetByFilterAsync(string name)
        {
            List<Company> retValue = await context.Companies
                                                  .Where(c => c.TradeName.Contains(name))
                                                  .Include(c => c.Country)
                                                  .ToListAsync();
            return retValue;
        }

        public async Task AddAsync(Company company)
        {
            context.Companies.Add(company);
            await context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Company company)
        {
            context.Companies.Update(company);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int companyID)
        {
            return await context.Companies
                                .Where(c => c.CompanyID == companyID)
                                .ExecuteDeleteAsync();
        }
    }
}
