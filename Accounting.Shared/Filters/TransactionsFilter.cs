namespace Accounting.Shared.Filters
{
    public class TransactionsFilter
    {
        public long TransactionID { get; set; } = 0;
        //TransactionDate
        public DateTime? TransactionDateFrom { get; set; }
        public DateTime? TransactionDateTo { get; set; }
        //IssuesDates
        public DateTime? IssueDateFrom { get; set; }
        public DateTime? IssueDateTo { get; set; }
        //DuesDates
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        //DocumentsTypes
        public List<short> DocumentsTypeID { get; set; } = new List<short>();
        //InternalNumbers
        public int? PrefixInternalNumberFrom { get; set; }
        public int? PrefixInternalNumberTo { get; set; }
        public int? InternalNumberFrom { get; set; }
        public int? InternalNumberTo { get; set; }
        //ExternalNumber (Document provider number)
        public int? PrefixExternalNumber { get; set; }
        public int? ExternalNumber { get; set; }
        //ExternalDates
        public DateTime? ExternalDateFrom { get; set; }
        public DateTime? ExternalDateTo { get; set; }
        //Entities (Client or Provider)
        public char? PrefixEntity { get; set; }
        public int? EntityIdFrom { get; set; }
        public int? EntityIdTo { get; set; }
        //PaymentsTypes
        public List<string> PaymentsTypeID { get; set; } = new List<string>();
        //BuySellConditions
        public List<int> BuySellConditionsID { get; set; } = new List<int>();
        //SpecializedJournals
        public List<short> SpecializedJournalsID { get; set; } = new List<short>();
        public int? BankAccountID { get; set; }
        public short? CoinID { get; set; }
        public int? OriginCashBoxID { get; set; }
        public int? DestinationCashBoxID { get; set; }
        //Total
        public decimal? TotalFrom { get; set; }
        public decimal? TotalTo { get; set; }
        //TotalTaxable
        public decimal? TotalTaxableFrom { get; set; }
        public decimal? TotalTaxableTo { get; set; }
        //TotalTaxes
        public decimal? TotalTaxesFrom { get; set; }
        public decimal? TotalTaxesTo { get; set; }
        public bool Eliminated { get; set; } = false;
        public bool Modified { get; set; } = false;
        public string? Observations { get; set; }
        public int? UserID { get; set; }
        //Request IP
        public string? IP { get; set; }
        //LastModification
        public DateTime? LastModificationFrom { get; set; }
        public DateTime? LastModificationTo { get; set; }
        public bool? ClosingJournalEntry { get; set; }
        public bool? ClosingJournal { get; set; }
        public bool? ProfitLossesClose { get; set; }
    }
}