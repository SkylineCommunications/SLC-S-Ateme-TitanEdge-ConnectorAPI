namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Models.Output
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	public class Output
	{
		[JsonProperty("sdi")]
		public Sdi Sdi { get; set; }
	}

	public class Sdi
	{
		[JsonProperty("connector")]
		public List<Connector> Connector { get; set; }
	}

	public class Connector
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("sdi")]
		public ConnectorSdi Sdi { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("additionalName")]
		public List<string> AdditionalName { get; set; }

		[JsonProperty("colorimetry")]
		public Colorimetry Colorimetry { get; set; }

		[JsonProperty("pixelRangeMode")]
		public string PixelRangeMode { get; set; }

		[JsonProperty("smpte2022_6")]
		public Smpte20226 Smpte20226 { get; set; }

		[JsonProperty("tr07")]
		public Tr07 Tr07 { get; set; }

		[JsonProperty("smpte2110")]
		public Smpte2110 Smpte2110 { get; set; }

		[JsonProperty("timeCodeControl")]
		public TimeCodeControl TimeCodeControl { get; set; }
	}
}
