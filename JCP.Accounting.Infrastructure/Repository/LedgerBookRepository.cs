using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Accounting.Shared.Enums;
using Accounting.Shared.Filters;

namespace Accounting.Infrastructure.Repository
{
    public class LedgerBookRepository : ILedgerBookRepository
    {
        private readonly AccountingDbContext context;
        private readonly ITransactionsRepository transactionsRepository;

        public LedgerBookRepository(AccountingDbContext context)
        {
            this.context = context;
            this.transactionsRepository = new TransactionsRepository(context);
        }

        public async Task<TransactionDTO?> GetByIDAsync(long transactionID)
        {
            List<RelatedTransactionEntity> relatedTransactionEntities =
                    new List<RelatedTransactionEntity> { RelatedTransactionEntity.JournalEntries };

            TransactionDTO? retValue = await transactionsRepository.GetByIDAsync(transactionID, relatedTransactionEntities);
            return retValue;
        }

        public async Task<List<TransactionDTO>> GetByFilterAsync(int companyID, TransactionsFilter filter)
        {
            List<RelatedTransactionEntity> relatedTransactionEntities =
                    new List<RelatedTransactionEntity> { RelatedTransactionEntity.JournalEntries };
            List<TransactionDTO> retValue = await transactionsRepository.GetByFilterAsync(companyID, filter);
            return retValue;
        }

        public async Task<long> AddAsync(TransactionDTO transactionDTO)
        {
            using (var dbTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    long id = await transactionsRepository.AddAsync(transactionDTO);
                    dbTransaction.Commit();
                    return id;
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        public async Task DeleteAsync(long transactionID, AuditTransactionDTO auditTransactionDTO)
        {
            await transactionsRepository.DeleteAsync(transactionID, auditTransactionDTO);
        }
    }
}
