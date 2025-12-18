using Accounting.Domain.DTO;

namespace Accounting.Infrastructure.Interfaces
{
    public interface IAccountsRepository
    {
        Task<List<AccountDTO>> GetAllAsync(int companyID, bool includeCoinInfo = false);
        Task<AccountDTO?> GetByIDAsync(int companyID, string accountID, bool includeCoinInfo = false);
        Task<List<AccountDTO>> GetByFilterAsync(int companyID, string data, bool includeCoinInfo = false);
        Task AddAsync(AccountDTO accountDTO);
        Task UpdateAsync(AccountDTO accountDTO);
        Task DeleteAsync(int companyID, string accountID);
    }
}
