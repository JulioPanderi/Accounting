using Accounting.Domain.DTO;
using System.Collections.Generic;

namespace Accounting.Application.Interfaces
{
    public interface ICoinsService : IDisposable
    {
        Task<List<CoinDTO>> GetAllAsync();
        Task<CoinDTO?> GetByIdAsync(short coinID);
        Task<short> AddAsync(CoinDTO coin);
        Task<int> UpdateAsync(CoinDTO coin);
        Task<int> DeleteAsync(short coinID);
    }
}

