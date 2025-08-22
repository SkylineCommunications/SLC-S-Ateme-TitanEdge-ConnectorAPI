namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Models.Input
{
	public class Smpte
	{
		public bool IsEnabled { get; set; }
		public string Address { get; set; }
		public int Port { get; set; }
		public string InterfaceName { get; set; }
		public string Skew { get; set; }
		public int CustomSkew { get; set; }
	}
}
