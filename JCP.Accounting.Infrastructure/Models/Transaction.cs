using Accounting.Infrastructure.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Infrastructure.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TransactionID { get; set; }
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        [Required]
        public short DocumentTypeID { get; set; }
        [Required]
        public int PrefixInternalNumber { get; set; }
        [Required]
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
        [Required]
        public int DocBookId { get; set; }
        public int? BankAccountID { get; set; }
        [Required]
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
        public string IP { get; set; }
        public DateTime? LastModification { get; set; }
        public decimal Quote { get; set; }
        public bool ClosingJournalEntry { get; set; }
        public bool ClosingJournal { get; set; }
        public bool ProfitLossesClose { get; set; }

        //Related entities
        public List<JournalEntry> JournalEntries { get; set; }
        public DocumentType? DocumentType { get; set; }
        public Company? Company { get; set; }
    }
}
