using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.Repository
{
    public class CoinsRepository : ICoinsRepository
    {
        private readonly AccountingDbContext context;

        public CoinsRepository(AccountingDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<CoinDTO>> GetAllAsync()
        {
            List<Coin> retValue = await context.Coins.OrderBy(c => c.CoinId).ToListAsync();
            return retValue.Select(c => Mappers.DtoMappers.MapCoinToDTO(c)).ToList();
        }

        public async Task<CoinDTO?> GetByIdAsync(short coinID)
        {
            Coin? retValue = await context.Coins.Where(c => c.CoinId == coinID).FirstOrDefaultAsync();
            return retValue == null ? null : Mappers.DtoMappers.MapCoinToDTO(retValue);
        }

        public async Task<List<CoinDTO>> GetByFilterAsync(string name)
        {
            List<Coin> retValue = await context.Coins
                                               .Where(c => c.Name.Contains(name))
                                               .OrderBy(c => c.Name)
                                               .ToListAsync();
            return retValue.Select(c => Mappers.DtoMappers.MapCoinToDTO(c)).ToList();
        }

        public async Task<short> AddAsync(CoinDTO coinDTO)
        {
            try
            {
                Coin coin = Mappers.EntityMappers.MapToCoin(coinDTO);
                context.Coins.Add(coin);
                await context.SaveChangesAsync();
                return coin.CoinId;
            }
            catch {
                throw;
            }
        }

        public async Task UpdateAsync(CoinDTO coinDTO)
        {
            try
            {
                Coin coin = Mappers.EntityMappers.MapToCoin(coinDTO);
                context.Coins.Update(coin);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(short coinID)
        {
            try
            {
                await context.Coins
                             .Where(c => c.CoinId == coinID)
                             .ExecuteDeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
