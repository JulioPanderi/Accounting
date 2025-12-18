using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Accounting.Shared.Enums;
using Accounting.Shared.Filters;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repository
{
    internal class JournalEntriesRepository : IJournalEntriesRepository
    {
        private readonly AccountingDbContext context;
        private readonly ITransactionsRepository transactionsRepository;

        public JournalEntriesRepository(AccountingDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            transactionsRepository = new TransactionsRepository(context);
        }

        public async Task<List<JournalEntryDTO>> GetByFilterAsync(int companyID, JournalEntriesFilter filter)
        {
            List<JournalEntry> retValue;
            List<RelatedTransactionEntity> includedEntities =
                            new List<RelatedTransactionEntity> { RelatedTransactionEntity.JournalEntries };
            IQueryable<Transaction> query = transactionsRepository.GetByFilter(companyID, filter, includedEntities);

            #region Journal Entries filters
            //Debit & Credit 
            query = query.Where(t => (t.JournalEntries.Any(j => j.Debit >= filter.DebitFrom && j.Debit <= filter.DebitTo)));
            query = query.Where(t => (t.JournalEntries.Any(j => j.Credit >= filter.CreditFrom && j.Debit <= filter.CreditTo)));
            #endregion
            retValue = await query.SelectMany(t => t.JournalEntries).ToListAsync();
            return retValue.Select(je => Mappers.DtoMappers.MapJournalEntryToDTO(je)).ToList(); 
        }

        public async Task<int> AddAsync(List<JournalEntryDTO> journalEntriesDTO)
        {
            try
            {
                List<JournalEntry> journalEntries = journalEntriesDTO.Select(je => Mappers.EntityMappers.MapToJournalEntry(je)).ToList();
                context.JournalEntries.AddRange(journalEntries);
                return await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}