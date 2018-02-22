using AllOutCoin.Models;
using Binance.API.Csharp.Client;
using Boukenken.Gdax;
using Jojatekok.PoloniexAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Xamarin.Auth;
using System.Linq;

namespace AllOutCoin.Services
{
    public class LoadData
    {
        private static object _lockobj = new Object();
        private static string key = "key";
        private static RSACryptoServiceProvider _rsaprovider = null;

        public static List<ExchangeEntity> myExchange = new List<ExchangeEntity>();

        public static async Task<RSACryptoServiceProvider> getRSAProvider()
        {
            if(_rsaprovider != null)
            {
                return _rsaprovider;
            }

            lock (_lockobj)
            {
                if (_rsaprovider != null)
                {
                    return _rsaprovider;
                }
                RSACryptoServiceProvider provider = null;
                var account = AccountStore.Create().FindAccountsForService(key).FirstOrDefault();
                if (account != null)
                {
                    provider = new RSACryptoServiceProvider();
                    provider.FromXmlString(account.Properties[key]);
                }
                else
                {
                    provider = new RSACryptoServiceProvider(2048);
                    IDictionary<string, string> dict = new Dictionary<string, string> { { key, provider.ToXmlString(true) } };
                    var accountData = new Xamarin.Auth.Account(key, dict);

                    AccountStore.Create().Save(accountData, key);
                    provider.FromXmlString(accountData.Properties[key]);

                }
                _rsaprovider = provider;
            }


            return _rsaprovider;
        }

        public static async  Task<Dictionary<String, Coin>> LoadCoinBalanceAsync(APILogin x)
        {

            Dictionary<String, Coin> myCoins = new Dictionary<string, Coin>();


            if (x.exchange == Exchanges.Gdax)
            {
                RequestAuthenticator gdax = new RequestAuthenticator(x.API_KEY, x.API_KEY_PASSWORD, x.API_KEY_SECRET);
                var accounts = await new AccountClient("https://api.gdax.com", gdax).ListAccountsAsync();
                
                foreach(var accnt in accounts.Value)
                {
                    Coin c = new Coin() { exchange = x.exchange, AvaliableValue = accnt.available, CoinName = accnt.currency.ToUpper(), OnOrderValue = accnt.hold };
                    myCoins.Add(accnt.currency.ToUpper(), c);
                }
            }
            else if (x.exchange == Exchanges.Poloniex)
            {
                var poly = new PoloniexClient(x.API_KEY,  x.API_KEY_SECRET);
                var bal = await poly.Wallet.GetBalancesAsync();

                foreach(var accnt in bal)
                {
                    Coin c = new Coin() { exchange = x.exchange, AvaliableValue = accnt.Value.QuoteAvailable, CoinName = accnt.Key.ToUpper(), OnOrderValue = accnt.Value.QuoteOnOrders };
                    myCoins.Add(accnt.Key.ToUpper(), c);
                }
            }
            else if (x.exchange == Exchanges.Binance)
            {
                var apiClient = new ApiClient(x.API_KEY, x.API_KEY_SECRET);
                var binanceClient = new BinanceClient(apiClient);
                var binanceData = await binanceClient.GetAccountInfo();
                foreach (var accnt in binanceData.Balances)
                {
                    Coin c = new Coin() { exchange=x.exchange, AvaliableValue = accnt.Free, CoinName = accnt.Asset.ToUpper(), OnOrderValue = accnt.Locked};
                    myCoins.Add(accnt.Asset.ToUpper(), c);
                }
            }

            

            return myCoins;
        }
    }
}
