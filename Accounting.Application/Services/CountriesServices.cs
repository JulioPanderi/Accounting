using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Accounting.Application.Services
{
    public class CountriesServices
    {
        private readonly ICountriesRepository countriesRepository;
        private readonly IServiceScope scope;
        private readonly ILogger<CountriesServices> logger;
        private bool disposedValue;

        public CountriesServices(ICountriesRepository countriesRepository, IServiceScopeFactory serviceScopeFactory, ILogger<CountriesServices> logger)
        {
            this.countriesRepository = countriesRepository;
            scope = serviceScopeFactory.CreateScope();
            this.logger = logger;
        }

        public async Task<List<CountryDTO>> GetAllAsync()
        {
            List<CountryDTO> retValue = await countriesRepository.GetAllAsync();
            return retValue;
        }

        public async Task<CountryDTO?> GetByIdAsync(short countryID)
        {
            CountryDTO? retValue = await countriesRepository.GetByIdAsync(countryID);
            return retValue;
        }

        public async Task<short> AddAsync(CountryDTO countryDTO)
        {
            short retValue = await countriesRepository.AddAsync(countryDTO);
            return retValue;
        }

        public async Task UpdateAsync(CountryDTO countryDTO)
        {
            await countriesRepository.UpdateAsync(countryDTO);
        }

        public async Task DeleteAsync(short countryID)
        {
            await countriesRepository.DeleteAsync(countryID);
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

