using Accounting.Shared.Filters;
using Accounting.Domain.DTO;

namespace Accounting.Infrastructure.Interfaces
{
    public interface IJournalEntriesRepository
    {
        Task<List<JournalEntryDTO>> GetByFilterAsync(int companyID, JournalEntriesFilter filter);
        Task<int> AddAsync(List<JournalEntryDTO> journalEntriesDTO);
    }
}
