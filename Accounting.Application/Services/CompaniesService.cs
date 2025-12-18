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
            List<CompanyDTO> retValue = await companiesRepository.GetAllAsync();
            return retValue;
        }

        public async Task<List<CompanyDTO>> GetByFilterAsync(string name)
        {
            List<CompanyDTO> retValue = await companiesRepository.GetByFilterAsync(name);
            return retValue;
        }

        public async Task<CompanyDTO?> GetByIdAsync(int companyID)
        {
            CompanyDTO? retValue = await companiesRepository.GetByIdAsync(companyID);
            return retValue;
        }

        public async Task UpdateAsync(CompanyDTO companyDTO)
        {
            await companiesRepository.UpdateAsync(companyDTO);
        }

        public async Task DeleteAsync(int companyID)
        {
            await companiesRepository.DeleteAsync(companyID);
        }

        public async Task<int> AddAsync(CompanyDTO companyDTO)
        {
            int retValue = await companiesRepository.AddAsync(companyDTO);
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
