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

        public async Task<List<Coin>> GetAllAsync()
        {
            List<Coin> retValue = await context.Coins.OrderBy(c => c.CoinId).ToListAsync();
            return retValue;
        }

        public async Task<Coin?> GetByIdAsync(short coinID)
        {
            Coin? retValue = await context.Coins.Where(c => c.CoinId == coinID).FirstOrDefaultAsync();
            return retValue;
        }

        public async Task<List<Coin>> GetByFilterAsync(string name)
        {
            List<Coin> retValue = await context.Coins
                                               .Where(c => c.Name.Contains(name))
                                               .OrderBy(c => c.Name)
                                               .ToListAsync();
            return retValue;
        }

        public async Task<short> AddAsync(Coin coin)
        {
            try
            {
                context.Coins.Add(coin);
                await context.SaveChangesAsync();
                return coin.CoinId;
            }
            catch {
                throw;
            }
        }

        public async Task<int> UpdateAsync(Coin coin)
        {
            try
            {
                context.Coins.Update(coin);
                return await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteAsync(short coinID)
        {
            try
            {
                return await context.Coins
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
