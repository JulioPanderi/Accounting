using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Infrastructure.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short CountryID { get; set; }
        [Required(AllowEmptyStrings=false)]
        public string Name { get; set; }=string.Empty;
        public short? OfficialCoinID { get; set; }
        public Coin? OfficialCoin { get; set; }
    }
}
