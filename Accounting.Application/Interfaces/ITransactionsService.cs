using Accounting.Domain.DTO;
using Accounting.Infrastructure.Models;
using Accounting.Shared.Enums;
using Accounting.Shared.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Interfaces
{
    internal interface ITransactionsService
    {
        Task<List<TransactionDTO>> GetByFiltersAsync(int companyID, TransactionsFilter filter, List<RelatedTransactionEntity> relatedEntities);
        Task<TransactionDTO?> GetByIDAsync(long transactionID);
        Task<long> AddAsync(TransactionDTO transactionDTO);
        Task UpdateAsync(TransactionDTO transactionDTO, AuditTransactionDTO auditTransactionDTO);
        Task DeleteAsync(long transactionID, AuditTransactionDTO auditTransactionDTO);
    }
}
