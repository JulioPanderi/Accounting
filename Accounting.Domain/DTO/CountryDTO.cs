namespace Accounting.Domain.DTO
{
    public class CountryDTO
    {
        public int CountryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public short? OfficialCoinID { get; set; }
        public CoinDTO? OfficialCoin { get; set; }
    }
}
