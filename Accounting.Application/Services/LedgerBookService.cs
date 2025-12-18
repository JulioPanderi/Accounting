using Accounting.Application.Interfaces;
using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Shared.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Accounting.Application.Services
{
    public class LedgerBookService : ILedgerBookService 
    {
        private readonly ILedgerBookRepository ledgerBookRepository;
        private readonly IServiceScope scope;
        private readonly ILogger<LedgerBookService> logger;
        private bool disposedValue;

        public LedgerBookService(ILedgerBookRepository ledgerBookRepository, IServiceScopeFactory serviceScopeFactory, ILogger<LedgerBookService> logger)
        {
            this.ledgerBookRepository = ledgerBookRepository;
            scope = serviceScopeFactory.CreateScope();
            this.logger = logger;
        }

        public async Task<List<TransactionDTO>> GetByFilterAsync(int companyID, TransactionsFilter filter)
        {
            try
            {
                List<TransactionDTO> transactionsDTO = await ledgerBookRepository.GetByFilterAsync(companyID, filter);
                return transactionsDTO;
            }
            catch
            {
                throw;
            }
        }

        public async Task<long> AddAsync(TransactionDTO transactionDTO)
        {
            try
            {
                long retValue = await ledgerBookRepository.AddAsync(transactionDTO);
                return retValue;
            }
            catch 
            {
                throw;
            }
        }

        public async Task DeleteAsync(long transactionID, AuditTransactionDTO auditTransactionDTO)
        {
            try
            {
                await ledgerBookRepository.DeleteAsync(transactionID, auditTransactionDTO);
            }
            catch
            {
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) scope.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
