namespace Accounting.Domain.DTO
{
    public class AuditTransactionDTO
    {
        public long AuditTransactionID { get; set; }
        public long TransactionID { get; set; }
        public int NewUserID { get; set; }
        public int OldUserID { get; set; }
        public DateTime AuditTransactionDate { get; set; }
        public string? AuditTransactionObservations { get; set; }
        public string NewIP { get; set; } = string.Empty;
        public string OldIP { get; set; } = string.Empty;
    }
}
