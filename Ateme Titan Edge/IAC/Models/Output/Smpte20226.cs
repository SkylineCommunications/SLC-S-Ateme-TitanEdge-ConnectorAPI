namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Models.Output
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	public class Smpte20226
	{
		[JsonProperty("ip")]
		public List<Ip> Ip { get; set; }

		[JsonProperty("systemClock")]
		public SystemClock SystemClock { get; set; }
	}

	public class Ip
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("isMandatory")]
		public bool IsMandatory { get; set; }

		[JsonProperty("source")]
		public string Source { get; set; }

		[JsonProperty("protocol")]
		public string Protocol { get; set; }

		[JsonProperty("destination")]
		public List<Destination> Destination { get; set; }

		[JsonProperty("ttl")]
		public int Ttl { get; set; }

		[JsonProperty("spoofing")]
		public Spoofing Spoofing { get; set; }

		[JsonProperty("smpte2022")]
		public Smpte2022 Smpte2022 { get; set; }

		[JsonProperty("zixi")]
		public Zixi Zixi { get; set; }

		[JsonProperty("srt")]
		public Srt Srt { get; set; }

		[JsonProperty("rist")]
		public Rist Rist { get; set; }

		[JsonProperty("filter")]
		public Filter Filter { get; set; }

		[JsonProperty("tsPacketsPerFrame")]
		public int TsPacketsPerFrame { get; set; }
	}

	public class Destination
	{
		[JsonProperty("interface")]
		public string Interface { get; set; }

		[JsonProperty("addressType")]
		public string AddressType { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("port")]
		public int Port { get; set; }

		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("isMuted")]
		public bool IsMuted { get; set; }

		[JsonProperty("tos")]
		public int Tos { get; set; }

		[JsonProperty("accelerationType")]
		public string AccelerationType { get; set; }
	}

	public class Spoofing
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("addressType")]
		public string AddressType { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }
	}

	public class Smpte2022
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("length")]
		public int Length { get; set; }

		[JsonProperty("depth")]
		public int Depth { get; set; }

		[JsonProperty("step")]
		public int Step { get; set; }

		[JsonProperty("isRowEnabled")]
		public bool IsRowEnabled { get; set; }
	}

	public class Zixi
	{
		[JsonProperty("channelName")]
		public string ChannelName { get; set; }

		[JsonProperty("maxLatency")]
		public int MaxLatency { get; set; }

		[JsonProperty("encryption")]
		public Encryption Encryption { get; set; }

		[JsonProperty("fecOverhead")]
		public int FecOverhead { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }
	}

	public class Encryption
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("mode")]
		public string Mode { get; set; }

		[JsonProperty("passPhrase")]
		public string PassPhrase { get; set; }

		[JsonProperty("secret")]
		public string Secret { get; set; }

		[JsonProperty("keyRotation")]
		public string KeyRotation { get; set; }
	}

	public class Srt
	{
		[JsonProperty("mode")]
		public string Mode { get; set; }

		[JsonProperty("streamId")]
		public string StreamId { get; set; }

		[JsonProperty("latency")]
		public int Latency { get; set; }

		[JsonProperty("bandwidthOverhead")]
		public int BandwidthOverhead { get; set; }

		[JsonProperty("encryption")]
		public Encryption Encryption { get; set; }

		[JsonProperty("isTsbpdEnabled")]
		public bool IsTsbpdEnabled { get; set; }

		[JsonProperty("outgoingPort")]
		public int OutgoingPort { get; set; }

		[JsonProperty("maximumConnectionCount")]
		public int MaximumConnectionCount { get; set; }

		[JsonProperty("addRtpHeader")]
		public bool AddRtpHeader { get; set; }
	}

	public class Rist
	{
		[JsonProperty("profile")]
		public string Profile { get; set; }

		[JsonProperty("mode")]
		public string Mode { get; set; }

		[JsonProperty("payloadSize")]
		public int PayloadSize { get; set; }

		[JsonProperty("buffer")]
		public int Buffer { get; set; }

		[JsonProperty("cname")]
		public string Cname { get; set; }

		[JsonProperty("authentication")]
		public Authentication Authentication { get; set; }

		[JsonProperty("encryption")]
		public Encryption Encryption { get; set; }

		[JsonProperty("maximumConnectionCount")]
		public int MaximumConnectionCount { get; set; }
	}

	public class Authentication
	{
		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }
	}

	public class Filter
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("service")]
		public List<string> Service { get; set; }

		[JsonProperty("outputBitrate")]
		public int OutputBitrate { get; set; }
	}
}
