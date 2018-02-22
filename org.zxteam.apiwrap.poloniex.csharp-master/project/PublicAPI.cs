namespace org.zxteam.apiwrap.poloniex
{
	using _internal;
	using data;

	using System;
	using System.Collections.Generic;
	using System.Diagnostics.Contracts;
	using System.Linq;
	using System.Net;
	using Newtonsoft.Json;

	public class PublicAPI
	{
		private static readonly DateTime UNIXBASETIME = new DateTime(1970, 1, 1);
		private const string DEFAULT_URL = "https://poloniex.com/public";

		private readonly Uri _url;

		public PublicAPI(string url = DEFAULT_URL)
		{
			this._url = new Uri(url);
		}

		public WebProxy Proxy { get; set; }

		[Pure]
		public ICurrency[] GetCurrencies()
		{
			string response = this.DownloadJSON("returnCurrencies");

			Dictionary<string, Currency> responseDict = JsonConvert.DeserializeObject<Dictionary<string, Currency>>(response);

			return responseDict.Select(s =>
			{
				s.Value.Code = s.Key; return s.Value;
			}).ToArray();
		}

		[Pure]
		public ITrade[] GetTradeHistory(string currencyPair, DateTime from, DateTime to)
		{
			long unixFrom = TranslateToUnixTime(from.ToUniversalTime());
			long unixTo = TranslateToUnixTime(to.ToUniversalTime());

			IDictionary<string, string> args = new Dictionary<string, string>() {
				{ "currencyPair", currencyPair},
				{ "start", unixFrom.ToString() },
				{ "end", unixTo.ToString() }
			};
			string response = this.DownloadJSON("returnTradeHistory", args);

			Trade[] friendlyResponse = JsonConvert.DeserializeObject<Trade[]>(response);

			if (friendlyResponse.Length >= 50000)
			{
				// Response records are overflow. 
				// Regarding Poloniex contract: up to 50,000 trades
				//   between a range specified in UNIX timestamps by 
				//   the \"start\" and \"end\". Please use smaller range.
				throw new PoloniexOverflowException();
			}
			return friendlyResponse;
		}

		[Pure]
		public ITicker[] GetTiker()
		{
			string response = this.DownloadJSON("returnTicker");

			Dictionary<string, Ticker> responseDict = JsonConvert.DeserializeObject<Dictionary<string, Ticker>>(response);

			return responseDict.Select(s =>
			{
				s.Value.CurrencyPair = s.Key; return s.Value;
			}).ToArray();
		}

		[Pure]
		private string DownloadJSON(string method, IDictionary<string, string> args = null)
		{
			using (WebClient wc = new WebClient())
			{
				wc.Proxy = this.Proxy ?? this.AutodetectWebProxy();
				wc.QueryString.Add("command", method);
				if (args != null)
				{
					foreach (var arg in args)
					{
						wc.QueryString.Add(arg.Key, arg.Value);
					}
				}
				string url = this._url.AbsoluteUri;
				var qq = wc.QueryString;
				return wc.DownloadString(url);
			}
		}

		[Pure]
		private WebProxy AutodetectWebProxy()
		{
			if (!WebRequest.DefaultWebProxy.IsBypassed(this._url))
			{
				String resolvedAddress = WebRequest.DefaultWebProxy.GetProxy(this._url).ToString();
				WebProxy wp = new WebProxy();
				ICredentials credentials = CredentialCache.DefaultNetworkCredentials;
				NetworkCredential credential = credentials.GetCredential(this._url, "Basic");
				wp.Credentials = credential;
				wp.Address = new Uri(resolvedAddress + @"proxy.pac");
				return wp;
			}
			return null;
		}

		[Pure]
		private static long TranslateToUnixTime(DateTime date) { return (long)date.Subtract(UNIXBASETIME).TotalSeconds; }

		public class PoloniexOverflowException : InvalidOperationException
		{
		}
	}
}
