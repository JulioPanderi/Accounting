using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repository
{
    public class AuditTransactionsRepository: IAuditTransactionsRepository
    {
        private readonly AccountingDbContext context;

        public AuditTransactionsRepository(AccountingDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(AuditTransactionDTO auditTransactionDTO)
        {
            try
            {
                AuditTransaction auditTransaction = Mappers.EntityMappers.MapToAuditTransaction(auditTransactionDTO);
                context.AuditTransactions.Add(auditTransaction);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
