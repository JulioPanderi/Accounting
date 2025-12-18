using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;

namespace Accounting.Application.Services
{
    internal class JournalEntriesService
    {
        private readonly IJournalEntriesRepository journalEntriesRepository;

        public JournalEntriesService(IJournalEntriesRepository journalEntriesRepository)
        {
            this.journalEntriesRepository = journalEntriesRepository;
        }

        public async Task<int> AddAsync(List<JournalEntryDTO> journalEntriesDTO)
        {
            return await journalEntriesRepository.AddAsync(journalEntriesDTO);
        }

        public async Task<int> UpdateAsync(List<JournalEntryDTO> journalEntriesDTO)
        {
            return await journalEntriesRepository.AddAsync(journalEntriesDTO);
        }
    }
}
