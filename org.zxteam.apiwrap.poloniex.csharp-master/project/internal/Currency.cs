#pragma warning disable 0649
namespace org.zxteam.apiwrap.poloniex._internal
{
	using Newtonsoft.Json;

	//	{
	//		"1CR": {
	//			"id":1,
	//			"name":"1CRedit",
	//			"txFee":"0.01000000",
	//			"minConf":3,
	//			"depositAddress":null,
	//			"disabled":0,
	//			"delisted":1,
	//			"frozen":0
	//		},
	//		"XXX" : {
	//			....
	//		},
	//		.....
	//	}
	[JsonObject]
	internal class Currency : data.ICurrency
	{
		private const string DatePattern = "yyyy-MM-dd HH:mm:ss";

		[JsonProperty]
		[JsonRequired]
		private long id;

		[JsonProperty]
		[JsonRequired]
		private string name;

		[JsonProperty]
		[JsonRequired]
		private decimal txFee;

		[JsonProperty]
		[JsonRequired]
		private string minConf;

		[JsonProperty]
		private string depositAddress;

		[JsonProperty]
		[JsonRequired]
		private int disabled;

		[JsonProperty]
		[JsonRequired]
		private int delisted;

		[JsonProperty]
		[JsonRequired]
		private int frozen;

		[JsonIgnore]
		public long Id { get { return this.id; } }

		[JsonIgnore]
		public string Name { get { return this.name; } }

		[JsonIgnore]
		public string Code { get; internal set; }

		[JsonIgnore]
		public decimal TxFee { get { return this.txFee; } }

		[JsonIgnore]
		public string MinConf { get { return this.minConf; } }

		[JsonIgnore]
		public string DepositAddress { get { return this.depositAddress; } }

		[JsonIgnore]
		public bool Disabled { get { return this.disabled == 1; } }

		[JsonIgnore]
		public bool Delisted { get { return this.delisted == 1; } }

		[JsonIgnore]
		public bool Frozen { get { return this.frozen == 1; } }

#if DEBUG
		public override string ToString()
		{
			return this.name;
		}
#endif

	}
}
#pragma warning restore 0649
