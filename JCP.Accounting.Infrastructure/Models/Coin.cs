using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Infrastructure.Models
{
    public class Coin
    {
        [Key]
        public short CoinId { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string Name { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false)]
        public string Symbol { get; set; } = string.Empty;
        public bool IsForegin { get; set; }
    }
}
