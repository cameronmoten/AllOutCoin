using System;

namespace AllOutCoin.Models
{


    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }

    public class APILogin
    {
        public Exchanges exchange { get; set; }
        public string API_KEY { get; set; }
        public string API_KEY_SECRET { get; set; }
        public string API_KEY_PASSWORD { get; set; }
        public string CoinType { get; set; }
    }

    public enum Exchanges
    {
        Wallet,
        Binance,
        Gdax,
        Wex,
        Poloniex
    }
    
    public class ExchangeEntity
    {
        public Exchanges exchange { get; set; }
        public String ShortName { get; set; }
        public String Desc { get; set; }
        public String Image { get; set; }
        public bool usesApiKey { get; set; }
        public bool usesApiKeySecret { get; set; }
        public bool usesApiKeyPassword { get; set; }
        
    }

    public class CoinPair
    {
        public Exchanges exchange { get; set; }

        public string CoinTicker_LEFT { get; set; }
        public string CoinTicker_RIGHT { get; set; }

        public decimal CoinPrice { get; set; } //last


    }

    public class Coin
    {
        public Exchanges exchange { get; set; }
        public String WalletAddress { get; set; }

        public string CoinTicker { get; set; }
        public string CoinName { get; set; }

        public decimal AvaliableValue { get; set; }
        public decimal OnOrderValue { get; set; }
        

}
}