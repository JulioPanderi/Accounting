using Accounting.Domain.DTO;
using Accounting.Infrastructure.Models;
using Riok.Mapperly.Abstractions;

namespace Accounting.Infrastructure.Mappers
{
    [Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
    internal static partial class DtoMappers
    {
        public static partial AccountDTO MapAccountToDTO(Account account);
        public static partial AuditTransactionDTO MapAuditTransactionToDTO(AuditTransaction auditTransaction);
        public static partial CoinDTO MapCoinToDTO(Coin coin);
        public static partial CountryDTO MapCountryToDTO(Country country);
        public static partial CompanyDTO MapCompanyToDTO(Company company);
        public static partial JournalEntryDTO MapJournalEntryToDTO(JournalEntry journalEntry);
        public static partial TransactionDTO MapTransactionToDTO(Transaction transaction);
    }

    [Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, AllowNullPropertyAssignment = false)]
    internal static partial class EntityMappers
    {
        public static partial Account MapToAccount(AccountDTO accountDTO);
        public static partial AuditTransaction MapToAuditTransaction(AuditTransactionDTO auditTransactionDTO);
        public static partial Coin MapToCoin(CoinDTO coinDTO);
        public static partial Country MapToCountry(CountryDTO countryDTO);
        public static partial JournalEntry MapToJournalEntry(JournalEntryDTO journalEntryDTO);
        internal static partial List<JournalEntry> _MapToJournalEntryList(List<JournalEntryDTO> journalEntriesDTO);
        internal static partial Transaction _MapToTransaction(TransactionDTO transactionDTO);

        [MapperIgnoreSource(nameof(CompanyDTO.CountryName))]
        [MapperIgnoreSource(nameof(CompanyDTO.CountryID))]
        public static partial Company MapToCompany(CompanyDTO companyDTO);

        public static Transaction MapToTransaction(TransactionDTO transactionDTO)
        {
            Transaction transaction = _MapToTransaction(transactionDTO);
            transaction.JournalEntries = _MapToJournalEntryList(transactionDTO.JournalEntries);
            return transaction;
        }
    }
}