using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace CoinTools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var test = DownloadAllCoins();
        }

        public class Coin
        {
            public int Id;
            public string Url;
            public string ImageUrl;
            public string Name;
            public string CoinName;
            public string FullName;
            public string Algorithm;
            public string ProofType;
            public int SortOrder;
            public bool Sponsored; //CryptoCompare Sponsor

        }

        static  bool DownloadAllCoins()
        {
            string url = "https://min-api.cryptocompare.com/data/all/coinlist";

            HttpClient x = new HttpClient();

            var jsonTask =  x.GetStringAsync(url);
            jsonTask.Wait();

            var json = jsonTask.Result;


            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            Dictionary<String, Coin> dataCoins = jsonObj.Data.ToObject<Dictionary<String, Coin>>();
            Console.WriteLine("Coins Loaded "+dataCoins.Keys.Count);
          
            Console.WriteLine(Directory.GetCurrentDirectory());
            Parallel.ForEach(dataCoins, data =>
            {
                    var dataTask = x.GetByteArrayAsync(jsonObj.BaseImageUrl.ToString() + "" + data.Value.ImageUrl);
                    dataTask.Wait();

                    File.WriteAllBytes("pics/" + data.Key + ".png", dataTask.Result);
                    Console.WriteLine("Downloaded " + data.Value.ImageUrl + " | " + dataTask.Result.Length);

            });
            return true;
        }
    }
}
