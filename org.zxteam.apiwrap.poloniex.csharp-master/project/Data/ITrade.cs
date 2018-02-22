namespace org.zxteam.apiwrap.poloniex.data
{
	using System;

	public enum TradeType
	{
		BUY,
		SELL
	}

	public interface ITrade
	{
		long GlobalTradeID { get; }
		long TradeID { get; }
		DateTime UtcDate { get; }
		TradeType Type { get; }
		decimal Rate { get; }
		decimal Amount { get; }
		decimal Total { get; }
	}
}
