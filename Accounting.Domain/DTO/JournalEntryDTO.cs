namespace Accounting.Domain.DTO
{
    public class JournalEntryDTO
    {
        public long TransactionID { get; set; }
        public short Sequence { get; set; }
        public int CompanyID { get; set; }
        public string AccountID { get; set; } = string.Empty;
        public decimal Debit { get; set; } = decimal.Zero;
        public decimal Credit { get; set; } = decimal.Zero;
        public decimal Amount { get; set; } = decimal.Zero;
        public string? Observations { get; set; }
    }
}
