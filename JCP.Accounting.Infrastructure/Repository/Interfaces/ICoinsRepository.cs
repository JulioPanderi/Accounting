using Accounting.Infrastructure.Models;
using System.Collections.Generic;

namespace Accounting.Infrastructure.Interfaces
{
    public interface ICoinsRepository
    {
        Task<List<Coin>> GetAllAsync();
        Task<Coin?> GetByIdAsync(short coinID);
        Task<List<Coin>> GetByFilterAsync(string name);
        Task<short> AddAsync(Coin coin);
        Task<int> UpdateAsync(Coin coin);
        Task<int> DeleteAsync(short coinID);
    }
}
