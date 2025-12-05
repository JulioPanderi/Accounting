using Accounting.Infrastructure.Filters;
using Accounting.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<List<Transaction>> GetByFiltersAsync(int companyID, TransactionsFilter filter);
        Task<Transaction?> GetByIDAsync(long transactionID);
        Task<long> AddAsync(Transaction transaction);
        Task<int> UpdateAsync(Transaction transaction);
        Task<int> DeleteAsync(Transaction transaction);
    }
}