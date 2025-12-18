using Accounting.Domain.DTO;

namespace Accounting.Infrastructure.Interfaces
{
    public interface IAuditTransactionsRepository
    {
        Task AddAsync(AuditTransactionDTO auditTransaction);
    }
}
