using Accounting.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.Interfaces
{
    public interface ILedgerBookRepository
    {
        Task AddAsync(Transaction transaction, List<JournalEntry> journalEntries);
        Task UpdateAsync(Transaction transaction, List<JournalEntry> journalEntries);
        Task DeleteAsync(Transaction transaction);
    }
}
