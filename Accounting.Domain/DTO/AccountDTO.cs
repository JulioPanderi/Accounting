using System;
using System.Diagnostics.CodeAnalysis;

namespace Accounting.Domain.DTO
{
    [ExcludeFromCodeCoverage]
    public class AccountDTO
    {
        public string AccountID { get; set; } = string.Empty;
        public int CompanyID { get; set; }
        public string Description { get; set; } = string.Empty;
        public short CoinID { get; set; }
        public bool IsResultAccount { get; set; }
        public bool IsClientAccount { get; set; }
        public bool IsProviderAccount { get; set; }
        public bool IsCashAccount { get; set; }
        public bool IsBankAccount { get; set; }
        public CoinDTO? Coin { get; set; }
    }
}