using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repository
{
    public class LedgerBookRepository : ILedgerBookRepository
    {
        private readonly AccountingDbContext context;
        private readonly ITransactionsRepository transactionsRepository;
        private readonly IJournalEntriesRepository journalEntriesRepository;

        public LedgerBookRepository(AccountingDbContext context)
        {
            this.context = context;
            this.transactionsRepository = new TransactionsRepository(context);
            this.journalEntriesRepository = new JournalEntriesRepository(context);
        }

        public async Task AddAsync(Transaction transaction, List<JournalEntry> journalEntries)
        {
            using (var dbTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    long id = await transactionsRepository.AddAsync(transaction);
                    journalEntries.ForEach(je => je.TransactionID = id);
                    await journalEntriesRepository.AddAsync(journalEntries);
                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        public async Task UpdateAsync(Transaction transaction, List<JournalEntry> journalEntries)
        {
            using (var dbTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    long id = await transactionsRepository.UpdateAsync(transaction);
                    journalEntries.ForEach(je => je.TransactionID = id);
                    await journalEntriesRepository.AddAsync(journalEntries);
                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            await transactionsRepository.DeleteAsync(transaction);
        }
    }
}
