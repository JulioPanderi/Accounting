namespace Accounting.Domain.DTO
{
    public class TransactionDTO
    {
        public long TransactionID { get; set; }
        public int CompanyID { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public short DocumentTypeID { get; set; }
        public int PrefixInternalNumber { get; set; }
        public int InternalNumber { get; set; }
        public int? PrefixExternalNumber { get; set; }
        public int? ExternalNumber { get; set; }
        public DateTime? ExternalDate { get; set; }
        public char? PrefixEntity { get; set; }
        public int? EntityId { get; set; }
        public string? PaymentTypeId { get; set; }
        public int? BuySellConditionID { get; set; }
        public short? TaxId { get; set; }
        public short? SpecializedJournalID { get; set; }
        public int DocBookId { get; set; }
        public int? BankAccountID { get; set; }
        public short CoinID { get; set; }
        public int? OriginCashBoxID { get; set; }
        public int? DestinationCashBoxID { get; set; }
        public decimal Total { get; set; }
        public decimal TotalTaxable { get; set; }
        public decimal TotalTaxes { get; set; }
        public bool Eliminated { get; set; }
        public bool Modified { get; set; }
        public string? Observations { get; set; }
        public int UserID { get; set; }
        public string IP { get; set; } = string.Empty;
        public DateTime? LastModification { get; set; }
        public decimal Quote { get; set; }
        //Used for closing books transactions
        public bool ClosingJournalEntry { get; set; }
        public bool ClosingJournal { get; set; }
        public bool ProfitLossesClose { get; set; }

        //Related Entities
        public List<JournalEntryDTO> JournalEntries { get; set; } = new List<JournalEntryDTO>();
    }
}
