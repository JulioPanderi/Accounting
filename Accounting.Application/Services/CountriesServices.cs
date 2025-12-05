using Accounting.Application.Interfaces;
using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

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
            List<CountryDTO> retValue = new List<CountryDTO>();
            var countries = await countriesRepository.GetAllAsync();
            if (countries != null)
            {
                retValue = (from c in countries select MapToDTO(c)).ToList();
            }
            return retValue;
        }

        public async Task<CountryDTO?> GetByIdAsync(short countryID)
        {
            Country? country = await countriesRepository.GetByIdAsync(countryID);
            CountryDTO? retValue = (country == null) ? null : MapToDTO(country);
            return retValue;
        }

        public async Task<int> AddAsync(CountryDTO countryDTO)
        {
            Country country = Map(countryDTO);
            return await countriesRepository.AddAsync(country);
        }

        public async Task<int> UpdateAsync(CountryDTO countryDTO)
        {
            Country country = Map(countryDTO);
            return await countriesRepository.UpdateAsync(country);
        }

        public async Task<int> DeleteAsync(short countryID)
        {
            return await countriesRepository.DeleteAsync(countryID);
        }

        internal static Country Map(CountryDTO countryDTO)
        {
            return new Country()
            {
                CountryID = countryDTO.CountryID,
                Name = countryDTO.Name,
                OfficialCoinID = countryDTO.OfficialCoinID
            };
        }

        internal static CountryDTO MapToDTO(Country country)
        {
            return new CountryDTO()
            {
                CountryID = country.CountryID,
                Name = country.Name,
                OfficialCoinID = country.OfficialCoinID
            };
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

