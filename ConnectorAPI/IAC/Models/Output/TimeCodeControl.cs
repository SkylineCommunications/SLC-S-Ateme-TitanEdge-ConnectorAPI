namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Models.Output
{
	using Newtonsoft.Json;

	public class TimeCodeControl
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("offset")]
		public int Offset { get; set; }
	}
}
