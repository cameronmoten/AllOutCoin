using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AllOutCoin.Models;
using Binance.API.Csharp.Client;
using Boukenken.Gdax;
using Jojatekok.PoloniexAPI;


[assembly: Xamarin.Forms.Dependency(typeof(AllOutCoin.Services.CoinDataStore))]
namespace AllOutCoin.Services
{
    public class CoinDataStore : IDataStore<Coin>
    {
        List<Coin> items;


        public CoinDataStore()
        {
            items = new List<Coin>();


            
        }

        public async Task<bool> AddItemAsync(Coin item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Coin item)
        {
            var _item = items.Where((Coin arg) => arg.CoinName == item.CoinName).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Coin item)
        {
            var _item = items.Where((Coin arg) => arg.CoinName == item.CoinName).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Coin> GetItemAsync(string id)
        {
            
            return await Task.FromResult(items.FirstOrDefault(s => s.CoinName == id));
        }

        public async Task<IEnumerable<Coin>> GetItemsAsync(bool forceRefresh = false)
        {

            var exchanges = await Settings.ExchangeListAsync();
            List<Dictionary<String, Coin>> saveData = new List<Dictionary<String, Coin>>();

            List<Coin> newItems = new List<Coin>();
           
            if(exchanges == null){

                newItems.Add(new Coin(){CoinName="Please Add."});
            }
            else
            {
                //Parallel has odd issues in Mono.Net
                var tasks = new List<Task<Dictionary<string,Coin>>>();




                foreach(var x in exchanges)
                {
                    //Workaround, oddities with Async Parllel
                    tasks.Add(Task.Factory.StartNew(async () =>
                    {
                        var data = await LoadData.LoadCoinBalanceAsync(x);
                        return data;
                    }).Unwrap());
                            
                }

                await Task.WhenAll(tasks.ToArray());
                tasks.ForEach(x=> saveData.Add(x.Result));
            }

            var groupbyName = saveData?.SelectMany(x => x.Values).GroupBy(x=> x.CoinName).OrderByDescending(x=> x.Sum(c=> c.AvaliableValue));

            foreach(var coinGroup in groupbyName)
            {
                Coin x = new Coin(){ CoinName = coinGroup.Key, AvaliableValue = coinGroup.Sum(c=> c.AvaliableValue),
                    OnOrderValue = coinGroup.Sum(c=> c.OnOrderValue)
                };
                if (x.AvaliableValue > 0 || x.OnOrderValue > 0)
                    newItems.Add(x);
            }

            items = newItems;
            

            return await Task.FromResult(newItems);
        }
    }
}