namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Models.Output
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	public class Smpte2110
	{
		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		[JsonProperty("isMandatory")]
		public bool IsMandatory { get; set; }

		[JsonProperty("configuration")]
		public string Configuration { get; set; }

		[JsonProperty("isRtcEnabled")]
		public bool IsRtcEnabled { get; set; }

		[JsonProperty("default")]
		public Smpte2110Default Default { get; set; }

		[JsonProperty("video")]
		public VideoWrapper Video { get; set; }

		[JsonProperty("audio")]
		public AudioWrapper Audio { get; set; }

		[JsonProperty("data")]
		public Data Data { get; set; }
	}

	public class Smpte2110Default
	{
		[JsonProperty("destination")]
		public List<Destination> Destination { get; set; }

		[JsonProperty("ttl")]
		public int Ttl { get; set; }

		[JsonProperty("video")]
		public Video Video { get; set; }

		[JsonProperty("audio")]
		public Audio Audio { get; set; }

		[JsonProperty("data")]
		public DataPayload Data { get; set; }
	}

	public class Video
	{
		[JsonProperty("rtpPayloadType")]
		public int RtpPayloadType { get; set; }

		[JsonProperty("mode")]
		public string Mode { get; set; }

		[JsonProperty("offset")]
		public Offset Offset { get; set; }

		[JsonProperty("smpte2110_22_jpegxs")]
		public Jpegxs Smpte211022Jpegxs { get; set; }
	}

	public class VideoWrapper
	{
		[JsonProperty("sender")]
		public Sender Sender { get; set; }
	}

	public class Sender
	{
		[JsonProperty("destination")]
		public List<Destination> Destination { get; set; }

		[JsonProperty("ttl")]
		public int Ttl { get; set; }

		[JsonProperty("video")]
		public Video Video { get; set; }
	}

	public class AudioWrapper
	{
		[JsonProperty("sender")]
		public List<AudioSender> Sender { get; set; }
	}

	public class AudioSender
	{
		[JsonProperty("destination")]
		public List<Destination> Destination { get; set; }

		[JsonProperty("ttl")]
		public int Ttl { get; set; }

		[JsonProperty("audio")]
		public Audio Audio { get; set; }
	}

	public class Data
	{
		[JsonProperty("ancillary")]
		public Ancillary Ancillary { get; set; }
	}

	public class Ancillary
	{
		[JsonProperty("sender")]
		public SenderData Sender { get; set; }
	}

	public class SenderData
	{
		[JsonProperty("destination")]
		public List<Destination> Destination { get; set; }

		[JsonProperty("ttl")]
		public int Ttl { get; set; }

		[JsonProperty("data")]
		public DataPayload Data { get; set; }
	}

	public class DataPayload
	{
		[JsonProperty("rtpPayloadType")]
		public int RtpPayloadType { get; set; }
	}
}
