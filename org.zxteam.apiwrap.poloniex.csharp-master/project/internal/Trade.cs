#pragma warning disable 0649
namespace org.zxteam.apiwrap.poloniex._internal
{
	using System;
	using System.Globalization;
	using Newtonsoft.Json;

	//	[
	//		{
	//			"globalTradeID":2036467,
	//			"tradeID":21387,
	//			"date":"2014-09-12 05:21:26",
	//			"type":"buy",
	//			"rate":"0.00008943",
	//			"amount":"1.27241180",
	//			"total":"0.00011379"
	//		},
	//		.....
	//	]
	[JsonObject]
	internal class Trade : data.ITrade
	{
		private const string DatePattern = "yyyy-MM-dd HH:mm:ss";

		[JsonProperty]
		[JsonRequired]
		private long globalTradeID;

		[JsonProperty]
		[JsonRequired]
		private long tradeID;

		[JsonProperty]
		[JsonRequired]
		private string date;

		[JsonProperty]
		[JsonRequired]
		private string type;

		[JsonProperty]
		[JsonRequired]
		private decimal rate;

		[JsonProperty]
		[JsonRequired]
		private decimal amount;

		[JsonProperty]
		[JsonRequired]
		private decimal total;

#if DEBUG
		public override string ToString()
		{
			return string.Format("Type:{0} Rate:{1} Amount:{2}", type, rate, amount);
		}
#endif

		[JsonIgnore]
		long data.ITrade.GlobalTradeID { get { return this.globalTradeID; } }
		[JsonIgnore]
		long data.ITrade.TradeID { get { return this.tradeID; } }
		[JsonIgnore]
		DateTime data.ITrade.UtcDate { get { return DateTime.ParseExact(this.date, DatePattern, CultureInfo.InvariantCulture, DateTimeStyles.None); } }
		[JsonIgnore]
		data.TradeType data.ITrade.Type
		{
			get
			{
				switch (this.type)
				{
					case "buy": return data.TradeType.BUY;
					case "sell": return data.TradeType.SELL;
					default: throw new NotSupportedException(this.type);
				}
			}
		}
		[JsonIgnore]
		decimal data.ITrade.Rate { get { return this.rate; } }
		[JsonIgnore]
		decimal data.ITrade.Amount { get { return this.amount; } }
		[JsonIgnore]
		decimal data.ITrade.Total { get { return this.total; } }
	}
}
#pragma warning restore 0649
