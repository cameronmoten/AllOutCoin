namespace org.zxteam.apiwrap.poloniex.data
{
	using System;

	public interface ITicker
	{
		string CurrencyPair { get; }
		long Id { get; }
		decimal Last { get; }
		decimal LowestAsk { get; }
		decimal HighestBid { get; }
		decimal PercentChange { get; }
		decimal BaseVolume { get; }
		decimal QuoteVolume { get; }
		bool IsFrozen { get; }
		decimal High24hr { get; }
		decimal Low24hr { get; }
	}
}
