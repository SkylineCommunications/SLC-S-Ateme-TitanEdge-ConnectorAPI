namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Models.Output
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	public class Tr07
	{
		[JsonProperty("ip")]
		public List<Ip> Ip { get; set; }

		[JsonProperty("pcrPid")]
		public int PcrPid { get; set; }

		[JsonProperty("jpegxs")]
		public Jpegxs Jpegxs { get; set; }

		[JsonProperty("audio")]
		public List<Audio> Audio { get; set; }

		[JsonProperty("systemClock")]
		public SystemClock SystemClock { get; set; }
	}

	public class Jpegxs
	{
		[JsonProperty("profile")]
		public string Profile { get; set; }

		[JsonProperty("bitsPerPixel")]
		public int BitsPerPixel { get; set; }

		[JsonProperty("psychovisual")]
		public Psychovisual Psychovisual { get; set; }
	}

	public class Psychovisual
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }
	}

	public class Audio
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("grouping")]
		public string Grouping { get; set; }

		[JsonProperty("size")]
		public string Size { get; set; }

		[JsonProperty("packetTime")]
		public string PacketTime { get; set; }

		[JsonProperty("rtpPayloadType")]
		public int RtpPayloadType { get; set; }

		[JsonProperty("mode")]
		public string Mode { get; set; }
	}
}
