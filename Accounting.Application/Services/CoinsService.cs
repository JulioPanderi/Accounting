using Accounting.Application.Interfaces;
using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Accounting.Application.Services
{
    public class CoinsService : ICoinsService
    {
        private readonly ICoinsRepository coinRepository;
        private readonly IServiceScope scope;
        private readonly ILogger<CoinsService> logger;
        private bool disposedValue;

        public CoinsService(ICoinsRepository coinRepository, IServiceScopeFactory serviceScopeFactory, ILogger<CoinsService> logger)
        {
            this.coinRepository = coinRepository;
            scope = serviceScopeFactory.CreateScope();
            this.logger = logger;
        }

        public async Task<List<CoinDTO>> GetAllAsync()
        {
            List<CoinDTO> retValue = await coinRepository.GetAllAsync();
            return retValue;
        }

        public async Task<CoinDTO?> GetByIdAsync(short coinID)
        {
            CoinDTO? retValue = await coinRepository.GetByIdAsync(coinID);
            return retValue;
        }

        public async Task<short> AddAsync(CoinDTO coinDTO)
        {
            short retValue = await coinRepository.AddAsync(coinDTO);
            return retValue;
        }

        public async Task UpdateAsync(CoinDTO coinDTO)
        {
            await coinRepository.UpdateAsync(coinDTO);
        }

        public async Task DeleteAsync(short coinID)
        {
            await coinRepository.DeleteAsync(coinID);
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
