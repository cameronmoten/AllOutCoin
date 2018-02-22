namespace org.zxteam.apiwrap.poloniex.data
{
	using System;

	public interface ICurrency
	{
		long Id { get; }
		string Code { get; }
		string Name { get; }
		decimal TxFee { get; }
		string MinConf { get; }
		string DepositAddress { get; }
		bool Disabled { get; }
		bool Delisted { get; }
		bool Frozen { get; }
	}
}
