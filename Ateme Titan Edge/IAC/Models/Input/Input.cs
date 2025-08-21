namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Models.Input
{
	public class Input
	{
		public bool IsEnabled { get; set; }
		public string Type { get; set; }
		public Ip Ip { get; set; }
		public Smpte Smpte { get; set; }
		public bool IsVbr { get; set; }
		public bool IsLowLatencyFec { get; set; }
		public string AccelerationType { get; set; }
	}
}
