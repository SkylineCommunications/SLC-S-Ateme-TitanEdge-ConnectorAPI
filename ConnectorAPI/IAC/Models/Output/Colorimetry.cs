namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Models.Output
{
	using Newtonsoft.Json;

	public class Colorimetry
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("conversion")]
		public string Conversion { get; set; }

		[JsonProperty("lut")]
		public Lut Lut { get; set; }
	}

	public class Lut
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("deprecatedNbcuLut")]
		public DeprecatedNbcuLut DeprecatedNbcuLut { get; set; }
	}

	public class DeprecatedNbcuLut
	{
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
