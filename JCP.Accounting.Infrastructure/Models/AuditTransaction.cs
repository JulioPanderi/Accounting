using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Infrastructure.Models
{
	public class AuditTransaction
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AuditTransactionID { get; set; }
        [Required]
        public long TransactionID { get; set; }
        public int OldUserID { get; set; }
        public int NewUserID { get; set; }
        [Required]
        public DateTime AuditTransactionDate { get; set; }
        public string? AuditTransactionObservations { get; set; }
        public string NewIP { get; set; } = string.Empty;
        public string OldIP { get; set; } = string.Empty;
    }
}
