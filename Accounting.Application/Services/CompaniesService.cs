using Accounting.Application.Interfaces;
using Accounting.Domain.DTO;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Accounting.Application.Services
{
    public class CompaniesService : ICompaniesService
    {

        private readonly ICompaniesRepository companiesRepository;
        private readonly IServiceScope scope;
        private readonly ILogger<CompaniesService> logger;
        private bool disposedValue;

        public CompaniesService(ICompaniesRepository companiesRepository, IServiceScopeFactory serviceScopeFactory, ILogger<CompaniesService> logger)
        {
            this.companiesRepository = companiesRepository;
            scope = serviceScopeFactory.CreateScope();
            this.logger = logger;
        }

        public async Task<List<CompanyDTO>> GetAllAsync()
        {
            List<CompanyDTO> retValue = new();
            var companies = await companiesRepository.GetAllAsync();
            if (companies != null)
            {
                retValue = (from c in companies select MapToDTO(c)).ToList();
            }
            return retValue;
        }

        public async Task<List<CompanyDTO>> GetByFilterAsync(string name)
        {
            List<CompanyDTO> retValue = new();
            var companies = await companiesRepository.GetByFilterAsync(name);
            if (companies != null)
            {
                retValue = (from c in companies select MapToDTO(c)).ToList();
            }
            return retValue;
        }

        public async Task<CompanyDTO?> GetByIdAsync(int companyID)
        {
            Company? company = await companiesRepository.GetByIdAsync(companyID);
            if (company != null)
            {
                return MapToDTO(company);
            }
            return null;
        }

        public async Task<int> UpdateAsync(CompanyDTO companyDTO)
        {
            Company company = Map(companyDTO);
            return await companiesRepository.UpdateAsync(company);
        }

        public async Task<int> DeleteAsync(int companyID)
        {
            return await companiesRepository.DeleteAsync(companyID);
        }

        public async Task AddAsync(CompanyDTO companyDTO)
        {
            Company company = Map(companyDTO);
            await companiesRepository.AddAsync(company);
        }

        internal static Company Map(CompanyDTO companyDTO)
        {
            Company retValue = new Company()
            {
                CompanyID = companyDTO.CompanyID,
                LegalName = companyDTO.LegalName,
                TradeName = companyDTO.TradeName,
                Phone = companyDTO.Phone,
                Email = companyDTO.Email,
                Address = companyDTO.Address,
                City = companyDTO.City,
                State = companyDTO.State,
                ZipCode = companyDTO.ZipCode,
                CountryID = companyDTO.CountryID
            };
            return retValue;
        }

        internal static CompanyDTO MapToDTO(Company company)
        {
            CompanyDTO retValue = new CompanyDTO()
            {
                CompanyID = company.CompanyID,
                LegalName = company.LegalName,
                TradeName = company.TradeName,
                Phone = company.Phone,
                Email = company.Email,
                Address = company.Address,
                City = company.City,
                State = company.State,
                ZipCode = company.ZipCode,
                CountryID = company.CountryID,
                CountryName = company.Country.Name
            };
            return retValue;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) scope.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
