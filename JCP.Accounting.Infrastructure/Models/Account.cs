using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Infrastructure.Models
{
    [PrimaryKey(nameof(AccountID), nameof(CompanyID))]
    public class Account
    {
        [Required(AllowEmptyStrings = false), Key]
        [Column(Order = 1)]
        public string AccountID { get; set; } = string.Empty;
        [Required, Key]
        [Column(Order = 2)]
        public int CompanyID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public short CoinID { get; set; }
        public Coin? Coin { get; set; }
        [Required]
        public bool IsResultAccount { get; set; }
        [Required]
        public bool IsClientAccount { get; set; }
        [Required]
        public bool IsProviderAccount { get; set; }
        [Required]
        public bool IsCashAccount { get; set; }
        [Required]
        public bool IsBankAccount { get; set; }
    }
}
