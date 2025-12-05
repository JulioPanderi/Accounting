using Accounting.Application.Interfaces;
using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
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
            List<CoinDTO> retValue = new List<CoinDTO>();
            var coins = await coinRepository.GetAllAsync();
            if (coins != null)
            {
                retValue = (from c in coins select MapToDTO(c)).ToList();
            }
            return retValue;
        }

        public async Task<CoinDTO?> GetByIdAsync(short coinID)
        {
            Coin? coin = await coinRepository.GetByIdAsync(coinID);
            CoinDTO? retValue = (coin == null ? null : MapToDTO(coin));
            return retValue;
        }

        public async Task<short> AddAsync(CoinDTO coinDTO)
        {
            Coin coin = Map(coinDTO);
            return await coinRepository.AddAsync(coin);
        }

        public async Task<int> UpdateAsync(CoinDTO coinDTO)
        {
            Coin coin = Map(coinDTO);
            return await coinRepository.UpdateAsync(coin);
        }

        public async Task<int> DeleteAsync(short coinID)
        {
            return await coinRepository.DeleteAsync(coinID);
        }

        internal static Coin Map(CoinDTO coinDTO)
        {
            Coin coin = new Coin()
            {
                CoinId = coinDTO.CoinId,
                Name = coinDTO.Name,
                Symbol = coinDTO.Symbol,
                IsForegin = coinDTO.IsForegin
            };
            return coin;
        }

        internal static CoinDTO MapToDTO(Coin coin)
        {
            CoinDTO retValue = new CoinDTO()
            {
                CoinId = coin.CoinId,
                Name = coin.Name,
                IsForegin = coin.IsForegin,
                Symbol = coin.Symbol
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
