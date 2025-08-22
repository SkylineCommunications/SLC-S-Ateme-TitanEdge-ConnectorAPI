using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Models.Input
{
	public class Ssm
	{
		public bool IsEnabled { get; set; }
		public string Mode { get; set; }
		public List<string> Address { get; set; }
	}
}
