using Accounting.Infrastructure.Filters;
using Accounting.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.Interfaces
{
    public interface IJournalEntriesRepository
    {
        Task<int> AddAsync(List<JournalEntry> journalEntries);
        Task<List<JournalEntry>> GetByFilterAsync(int companyID, JournalEntriesFilter filter);
        Task<Transaction?> GetByTransactionIdAsync(long transactionID, bool includeEliminated = false);
        Task<int> UpdateAsync(List<JournalEntry> journalEntries);
    }
}
