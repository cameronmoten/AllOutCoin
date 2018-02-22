namespace Jojatekok.PoloniexAPI.WalletTools
{
    public interface IBalance
    {
        decimal QuoteAvailable { get; }
        decimal QuoteOnOrders { get; }
        decimal BitcoinValue { get; }
    }
}
