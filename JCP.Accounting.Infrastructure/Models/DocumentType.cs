using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.Models
{
    public class DocumentType
    {
        [Key]
        public short DocumentTypeID { get; set; }
        [Required]
        public string DocumentName { get; set; } = string.Empty;
        [Required]
        public string ShortName { get; set; } = string.Empty;
        [Required]
        public bool DiscriminateTaxes { get; set; }
        public short? Sign { get; set; }
        [Required]
        public bool UseItems { get; set; } = false;
        public short? ItemSign { get; set; }
        [Required]
        public bool IsCreditNote { get; set; }
        [Required]
        public bool IsDebitNote { get; set; }
        [Required]
        public bool IsBankDebit { get; set; }
        [Required]
        public bool IsJournal { get; set; }
        [Required]
        public bool GenerateJournal { get; set; }
        [Required]
        public bool IsInvoice { get; set; }
        [Required]
        public bool IsForSell { get; set; }
        [Required]
        public bool IsForBuy { get; set; }
    }
}
