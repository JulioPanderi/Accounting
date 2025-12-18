using Accounting.Domain.DTO;
using Accounting.Infrastructure.Models;
using Accounting.Shared.Enums;
using Accounting.Shared.Filters;

namespace Accounting.Infrastructure.Interfaces
{
    public interface ITransactionsRepository
    {
        IQueryable<Transaction> GetByFilter(int companyID, TransactionsFilter filter, List<RelatedTransactionEntity> includedEntities);
        Task<List<TransactionDTO>> GetByFilterAsync(int companyID, TransactionsFilter filter, List<RelatedTransactionEntity>? relatedTransactionEntities = null);
        Task<TransactionDTO?> GetByIDAsync(long transactionID, List<RelatedTransactionEntity>? relatedTransactionEntities = null);
        Task<long> AddAsync(TransactionDTO transactionDTO);
        Task UpdateAsync(TransactionDTO transactionDTO, AuditTransactionDTO auditTransactionDTO);
        Task DeleteAsync(long transactionID, AuditTransactionDTO auditTransactionDTO);
    }
}