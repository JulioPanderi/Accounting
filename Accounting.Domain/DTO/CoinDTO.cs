using System;
using System.Diagnostics.CodeAnalysis;

namespace Accounting.Domain.DTO
{
    [ExcludeFromCodeCoverage]
    public class CoinDTO
    {
        public short CoinId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public bool IsForegin { get; set; }
    }
}
