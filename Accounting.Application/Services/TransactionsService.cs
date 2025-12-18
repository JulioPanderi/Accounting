using Accounting.Application.Interfaces;
using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repository;
using Accounting.Shared.Enums;
using Accounting.Shared.Filters;

namespace Accounting.Application.Services
{
    /// <summary>
    /// Service class for the Transactions. Every document generated, should use this class as main, and then write their corresponding children
    /// </summary>
    /// <remarks>
    /// Transactions records are not allowed to be modified or deleted. To modified or delete a transaction, a new one should be created and mark the previous one as deleted or modified. Also an auditory record sholud be generated
    /// </remarks>
    internal class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository transactionsRepository;

        public TransactionsService(ITransactionsRepository transactionsRepository)
        {
            this.transactionsRepository = transactionsRepository;
        }

        public async Task<List<TransactionDTO>> GetByFiltersAsync(int companyID, TransactionsFilter filter, List<RelatedTransactionEntity> relatedEntities)
        {
            List<TransactionDTO> retValue = await transactionsRepository.GetByFilterAsync(companyID, filter, relatedEntities);
            return retValue;
        }

        public async Task<TransactionDTO?> GetByIDAsync(long transactionID)
        {
            TransactionDTO? retValue = await transactionsRepository.GetByIDAsync(transactionID);
            return retValue;
        }

        public async Task<long> AddAsync(TransactionDTO transactionDTO)
        {
            try
            {
                long retValue = await transactionsRepository.AddAsync(transactionDTO);
                return retValue;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update method only allowed for close operations
        /// </summary>
        /// <param name="transactionDTO"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TransactionDTO transactionDTO, AuditTransactionDTO auditTransactionDTO)
        {
            try
            {
                await transactionsRepository.UpdateAsync(transactionDTO, auditTransactionDTO);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(long transactionID, AuditTransactionDTO auditTransactionDTO)
        {
            await transactionsRepository.DeleteAsync(transactionID, auditTransactionDTO);
        }
    }
}
