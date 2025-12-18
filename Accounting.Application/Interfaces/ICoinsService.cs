using Accounting.Domain.DTO;
using System.Collections.Generic;

namespace Accounting.Application.Interfaces
{
    public interface ICoinsService : IDisposable
    {
        Task<List<CoinDTO>> GetAllAsync();
        Task<CoinDTO?> GetByIdAsync(short coinID);
        Task<short> AddAsync(CoinDTO coin);
        Task UpdateAsync(CoinDTO coin);
        Task DeleteAsync(short coinID);
    }
}

