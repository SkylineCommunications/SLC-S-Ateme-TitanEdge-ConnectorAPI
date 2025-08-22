namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Models.Output
{
	using Newtonsoft.Json;

	public class ConnectorSdi
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("genlock")]
		public Genlock Genlock { get; set; }

		[JsonProperty("systemClock")]
		public SystemClock SystemClock { get; set; }
	}

	public class Genlock
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("offset")]
		public Offset Offset { get; set; }
	}

	public class Offset
	{
		[JsonProperty("lines")]
		public int Lines { get; set; }

		[JsonProperty("pixels")]
		public int Pixels { get; set; }

		[JsonProperty("time")]
		public int Time { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}

	public class SystemClock
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("offset")]
		public Offset Offset { get; set; }
	}
}

