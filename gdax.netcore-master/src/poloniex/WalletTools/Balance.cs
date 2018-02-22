using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.WalletTools
{
    public class Balance : IBalance
    {
        [JsonProperty("available")]
        public decimal QuoteAvailable { get; private set; }
        [JsonProperty("onOrders")]
        public decimal QuoteOnOrders { get; private set; }
        [JsonProperty("btcValue")]
        public decimal BitcoinValue { get; private set; }
    }
}
