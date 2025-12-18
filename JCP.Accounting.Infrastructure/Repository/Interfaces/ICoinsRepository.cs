using Accounting.Domain.DTO;

namespace Accounting.Infrastructure.Interfaces
{
    public interface ICoinsRepository
    {
        Task<List<CoinDTO>> GetAllAsync();
        Task<CoinDTO?> GetByIdAsync(short coinID);
        Task<List<CoinDTO>> GetByFilterAsync(string name);
        Task<short> AddAsync(CoinDTO coinDTO);
        Task UpdateAsync(CoinDTO coinDTO);
        Task DeleteAsync(short coinID);
    }
}
