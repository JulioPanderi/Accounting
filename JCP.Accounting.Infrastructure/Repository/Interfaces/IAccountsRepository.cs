using Accounting.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.Interfaces
{
    public interface IAccountsRepository
    {
        Task<List<Account>> GetAllAsync(int companyID, bool includeCoinInfo = false);
        Task<Account?> GetByIDAsync(int companyID, string accountID, bool includeCoinInfo = false);
        Task<List<Account>> GetByFilterAsync(int companyID, string data, bool includeCoinInfo = false);
        Task<int> UpdateAsync(Account account);
        Task<int> DeleteAsync(int companyID, string accountID);
        Task AddAsync(Account account);
    }
}
