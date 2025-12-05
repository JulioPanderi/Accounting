using Accounting.Infrastructure.Filters;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repository
{
    public class JournalEntriesRepository : IJournalEntriesRepository
    {
        private readonly AccountingDbContext context;

        public JournalEntriesRepository(AccountingDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<JournalEntry>> GetByFilterAsync(int companyID, JournalEntriesFilter filter)
        {
            List<JournalEntry> retValue;
            TransactionRespositoryHelper helper = new TransactionRespositoryHelper(context);
            IQueryable<Transaction> query = helper.GetByFilter(companyID, filter);

            #region filters
            //Debit & Credit 
            query = query.Where(t => (t.JournalEntries.Any(j => j.Debit >= filter.DebitFrom && j.Debit <= filter.DebitTo)));
            query = query.Where(t => (t.JournalEntries.Any(j => j.Credit >= filter.CreditFrom && j.Debit <= filter.CreditTo)));
            #endregion
            retValue = await query.SelectMany(je => je.JournalEntries).ToListAsync();
            return retValue;
        }

        public async Task<Transaction?> GetByTransactionIdAsync(long transactionID, bool includeEliminated = false)
        {
            Transaction? retValue = await context.Transactions.Where(t => t.TransactionID == transactionID).FirstOrDefaultAsync();
            return retValue;
        }

        public async Task<int> AddAsync(List<JournalEntry> journalEntries)
        {
            try
            {
                int retValue = 0;
                await Task.Run(async () =>
                {
                    context.JournalEntries.AddRange(journalEntries);
                    retValue = await context.SaveChangesAsync();
                });
                return retValue;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(List<JournalEntry> journalEntries)
        {
            try
            {
                int retValue = 0;
                await Task.Run(async () =>
                {
                    context.JournalEntries.UpdateRange(journalEntries);
                    retValue = await context.SaveChangesAsync();
                });
                return retValue;
            }
            catch
            {
                throw;
            }
        }
    }
}