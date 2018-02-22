using System;
using System.Collections.Generic;
using System.Text;

namespace AllOutCoin.Models
{
    public class StaticData
    {
        public static Dictionary<Exchanges, ExchangeEntity> supportedExchanges = new Dictionary<Exchanges, ExchangeEntity>()
        {
            { Exchanges.Binance, new ExchangeEntity() { ShortName="Binance", Desc="Working.", usesApiKey= true, usesApiKeyPassword= false,usesApiKeySecret=true,exchange=Exchanges.Binance, Image="binance.png" } },
            { Exchanges.Gdax, new ExchangeEntity() { ShortName="Gdax", Desc="Working.", usesApiKey= true, usesApiKeyPassword= true,usesApiKeySecret=true,exchange=Exchanges.Gdax, Image="gdax.png" } },
            { Exchanges.Poloniex, new ExchangeEntity() { ShortName="Poloniex", Desc="Working.", usesApiKey= true, usesApiKeyPassword= false,usesApiKeySecret=true,exchange=Exchanges.Poloniex, Image="poloniex.png" } }
        };
    }
}
