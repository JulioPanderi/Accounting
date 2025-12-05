using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Domain.Filters
{
    public class JournalEntriesFilterDTO : TransactionsFilterDTO
    {
        public string? AccountFrom { get; set; }
        public string? AccountTo { get; set; }
        public decimal DebitFrom { get; set; } = decimal.Zero;
        public decimal DebitTo { get; set; } = decimal.MaxValue;
        public decimal CreditFrom { get; set; } = decimal.Zero;
        public decimal CreditTo { get; set; } = decimal.MaxValue;
    }
}
