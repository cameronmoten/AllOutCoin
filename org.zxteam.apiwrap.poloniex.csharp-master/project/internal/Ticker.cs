#pragma warning disable 0649
namespace org.zxteam.apiwrap.poloniex._internal
{
	using Newtonsoft.Json;

	//	{
	//		"BTC_BBR":{
	//			"id":6,
	//			"last":"0.00027827",
	//			"lowestAsk":"0.00027827",
	//			"highestBid":"0.00027799",
	//			"percentChange":"-0.09313997",
	//			"baseVolume":"25.21567258",
	//			"quoteVolume":"87401.61506595",
	//			"isFrozen":"0",
	//			"high24hr":"0.00032000",
	//			"low24hr":"0.00027000"
	//		},
	//		"XXX_YYY" : {
	//			....
	//		},
	//		.....
	//	}
	[JsonObject]
	internal class Ticker : data.ITicker
	{
		[JsonProperty]
		[JsonRequired]
		private long id;

		[JsonProperty]
		[JsonRequired]
		private decimal last;

		[JsonProperty]
		[JsonRequired]
		private decimal lowestAsk;

		[JsonProperty]
		[JsonRequired]
		private decimal highestBid;

		[JsonProperty]
		[JsonRequired]
		private decimal percentChange;

		[JsonProperty]
		[JsonRequired]
		private decimal baseVolume;

		[JsonProperty]
		[JsonRequired]
		private decimal quoteVolume;

		[JsonProperty]
		[JsonRequired]
		private int isFrozen;

		[JsonProperty]
		[JsonRequired]
		private decimal high24hr;

		[JsonProperty]
		[JsonRequired]
		private decimal low24hr;

		[JsonIgnore]
		public long Id { get { return this.id; } }

		[JsonIgnore]
		public string CurrencyPair { get; internal set; }

		[JsonIgnore]
		public decimal Last { get { return this.last; } }

		[JsonIgnore]
		public decimal LowestAsk { get { return this.lowestAsk; } }

		[JsonIgnore]
		public decimal HighestBid { get { return this.highestBid; } }

		[JsonIgnore]
		public decimal PercentChange { get { return this.percentChange; } }

		[JsonIgnore]
		public decimal BaseVolume { get { return this.baseVolume; } }

		[JsonIgnore]
		public decimal QuoteVolume { get { return this.quoteVolume; } }

		[JsonIgnore]
		public bool IsFrozen { get { return this.isFrozen == 1; } }

		[JsonIgnore]
		public decimal High24hr { get { return this.highestBid; } }

		[JsonIgnore]
		public decimal Low24hr { get { return this.low24hr; } }

#if DEBUG
		public override string ToString()
		{
			return this.CurrencyPair;
		}
#endif

	}
}
#pragma warning restore 0649
