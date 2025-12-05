using Accounting.Application.Interfaces;
using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<JournalEntry> journalEntries = journalEntriesDTO.Select(je => Map(je)).ToList();
            for(short i = 1; i <= journalEntries.Count; i++) 
            {
                journalEntries[i-1].Sequence = i;
            }
            return await journalEntriesRepository.AddAsync(journalEntries);
        }

        public async Task<int> UpdateAsync(List<JournalEntryDTO> journalEntriesDTO)
        {
            List<JournalEntry> journalEntries = journalEntriesDTO.Select(je => Map(je)).ToList();
            return await journalEntriesRepository.AddAsync(journalEntries);
        }

        internal static JournalEntry Map(JournalEntryDTO journalEntryDTO)
        {
            JournalEntry retValue = new JournalEntry()
            {
                TransactionID = journalEntryDTO.TransactionID,
                Sequence = journalEntryDTO.Sequence,
                CompanyID = journalEntryDTO.CompanyID,
                AccountID = journalEntryDTO.AccountID,
                Debit = journalEntryDTO.Debit,
                Credit = journalEntryDTO.Credit,
                Amount = journalEntryDTO.Amount,
                Observations = journalEntryDTO.Observations
            };
            return retValue;
        }

        internal static JournalEntryDTO MapToDTO(JournalEntry journalEntry)
        {
            JournalEntryDTO retValue = new JournalEntryDTO()
            {
                TransactionID = journalEntry.TransactionID,
                Sequence = journalEntry.Sequence,
                CompanyID = journalEntry.CompanyID,
                AccountID = journalEntry.AccountID,
                Debit = journalEntry.Debit,
                Credit = journalEntry.Credit,
                Amount = journalEntry.Amount,
                Observations = journalEntry.Observations
            };
            return retValue;
        }
    }
}
