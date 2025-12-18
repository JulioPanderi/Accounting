using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Infrastructure.Repository
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly AccountingDbContext context;

        public CompaniesRepository(AccountingDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<CompanyDTO>> GetAllAsync()
        {
            List<CompanyDTO> retValue = await (from Company co in context.Companies
                                               join Country c in context.Countries on co.CountryID equals c.CountryID
                                               select new CompanyDTO
                                               {
                                                     CompanyID = co.CompanyID,
                                                     LegalName = co.LegalName,
                                                     TradeName = co.TradeName,
                                                     Phone = co.Phone,
                                                     Email = co.Email,
                                                     Address = co.Address,
                                                     City = co.City,
                                                     State = co.State,
                                                     ZipCode = co.ZipCode,
                                                     CountryID = co.CountryID,
                                                     CountryName = c.Name
                                               }).ToListAsync();
            return retValue;
        }

        public async Task<CompanyDTO?> GetByIdAsync(int companyID)
        {
            CompanyDTO? retValue = await (from Company co in context.Companies
                                          join Country c in context.Countries on co.CountryID equals c.CountryID
                                          select new CompanyDTO
                                          {
                                                CompanyID = co.CompanyID,
                                                LegalName = co.LegalName,
                                                TradeName = co.TradeName,
                                                Phone = co.Phone,
                                                Email = co.Email,
                                                Address = co.Address,
                                                City = co.City,
                                                State = co.State,
                                                ZipCode = co.ZipCode,
                                                CountryID = co.CountryID,
                                                CountryName = c.Name
                                          }).FirstOrDefaultAsync();
            return retValue;
        }

        public async Task<List<CompanyDTO>> GetByFilterAsync(string name)
        {
            List<CompanyDTO> retValue = await (from Company co in context.Companies
                                               join Country c in context.Countries on co.CountryID equals c.CountryID
                                               where co.TradeName.Contains(name)
                                               select new CompanyDTO
                                               {
                                                    CompanyID = co.CompanyID,
                                                    LegalName = co.LegalName,
                                                    TradeName = co.TradeName,
                                                    Phone = co.Phone,
                                                    Email = co.Email,
                                                    Address = co.Address,
                                                    City = co.City,
                                                    State = co.State,
                                                    ZipCode = co.ZipCode,
                                                    CountryID = co.CountryID,
                                                    CountryName = c.Name
                                               }).ToListAsync();
            return retValue;
        }

        public async Task<int> AddAsync(CompanyDTO companyDTO)
        {
            Company company = Mappers.EntityMappers.MapToCompany(companyDTO);
            context.Companies.Add(company);
            await context.SaveChangesAsync();
            return company.CompanyID;
        }

        public async Task UpdateAsync(CompanyDTO companyDTO)
        {
            Company company = Mappers.EntityMappers.MapToCompany(companyDTO);
            context.Companies.Update(company);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int companyID)
        {
            await context.Companies
                         .Where(c => c.CompanyID == companyID)
                         .ExecuteDeleteAsync();
        }
    }
}