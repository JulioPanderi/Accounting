using Accounting.Domain.DTO;
using Accounting.Shared.Filters;

namespace Accounting.Application.Interfaces
{
    public interface ILedgerBookService : IDisposable
    {
        Task<List<TransactionDTO>> GetByFilterAsync(int companyID, TransactionsFilter filter);
        Task<long> AddAsync(TransactionDTO transaction);
        Task DeleteAsync(long transactionID, AuditTransactionDTO auditTransactionDTO);
    }
}
