using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.Models
{
    public class JournalEntry
    {
        [Key]
        [Column(Order = 1)]
        public long TransactionID {  get; set; }
        [Key]
        [Column(Order = 2)]
        public short Sequence { get; set; }
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public string AccountID {  get; set; } = string.Empty;
        [Required]
        public decimal Debit { get; set; } = decimal.Zero;
        [Required]
        public decimal Credit { get; set; } = decimal.Zero;
        public decimal Amount { get; set; } = decimal.Zero;
        public string? Observations { get; set; }
    }
}
