using Newtonsoft.Json;

namespace Models.DTOs
{
    public class CoinPriceDTO
    {
        [JsonProperty(PropertyName = "market_data")]
        public MarketData MarketData { get; set; }
    }

    public class MarketData
    {
        [JsonProperty(PropertyName = "eur")]
        public decimal Euro { get; set; }
    }
}
