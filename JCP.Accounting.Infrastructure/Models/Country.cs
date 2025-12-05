using System.ComponentModel.DataAnnotations;

namespace Accounting.Infrastructure.Models
{
    public class Country
    {
        [Key]
        public int CountryID { get; set; }
        [Required(AllowEmptyStrings=false)]
        public string Name { get; set; }=string.Empty;
        public short? OfficialCoinID { get; set; }
        public Coin? OfficialCoin { get; set; }
    }
}
