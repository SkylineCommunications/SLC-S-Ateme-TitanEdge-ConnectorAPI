using System.Text.Json.Serialization;

namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Models.Input
{
	public class Ip
	{
		public string Address { get; set; }
		public int Port { get; set; }

		[JsonPropertyName("interface")]
		public string InterfaceName { get; set; }
		public bool IsFecEnabled { get; set; }
		public int BufferDuration { get; set; }
		public Ssm Ssm { get; set; }
		public Smpte Smpte2022_7 { get; set; }
	}
}
