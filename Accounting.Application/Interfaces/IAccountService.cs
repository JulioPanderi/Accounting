using Accounting.Domain.DTO;

namespace Accounting.Application.Interfaces
{
    public interface IAccountsService: IDisposable
    {
        Task<List<AccountDTO>> GetAllAsync(int companyID);
        Task<AccountDTO?> GetByIdAsync(int companyID, string accountID);
        Task<int> UpdateAsync(AccountDTO accountDTO);
        Task<int> DeleteAsync(int companyID, string accountID);
        Task AddAsync(AccountDTO accountDTO);
    }
}
