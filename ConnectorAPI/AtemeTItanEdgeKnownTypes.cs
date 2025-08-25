namespace Skyline.DataMiner.Utils.AtemeTitanEdge
{
	using System;
	using System.Collections.Generic;

	using static Skyline.DataMiner.Utils.AtemeTitanEdge.InputConfiguration;
	using static Skyline.DataMiner.Utils.AtemeTitanEdge.OutputConfiguration;

	public static class AtemeTitanEdgeKnownTypes
	{
		/// <summary>
		/// Gets the list of known types for the Ateme Titan Edge configuration.
		/// </summary>
		/// <remarks>
		/// This list is used for serialization and deserialization purposes.
		/// </remarks>
		/// <returns>A list of known types.</returns>
		public static List<Type> KnownTypes => new List<Type>
		{
			typeof(ConfigBaseMessage),
			typeof(EnableInputMessage),
			typeof(SetIpAddressMessage),
			typeof(SetInputTypeMessage),
			typeof(SetIpPortMessage),
			typeof(SetInterfaceMessage),
			typeof(EnableInputFecMessage),
			typeof(SetBufferDurationMessage),
			typeof(EnableSourceSpecificMulticastMessage),
			typeof(SetSourceSpecificMulticastModeMessage),
			typeof(EnableSmpte20227Message),
			typeof(SetSmpte20227IpAddressMessage),
			typeof(SetSmpte20227PortMessage),
			typeof(SetSmpte20227InterfaceMessage),
			typeof(SetSmpte20227SkewMessage),
			typeof(SetSmpte20227CustomSkewMessage),
			typeof(EnableInputVbrMessage),
			typeof(EnableLowLatencyFecMessage),
			typeof(SetAccelerationTypeMessage),
			typeof(OutputConnectorTypeMessage),
			typeof(OutputConnectorNameMessage),
			typeof(OutputConnectorColorimetryMessage),
			typeof(OutputConnectorColorimetryConversionMessage),
			typeof(OutputConnectorIutMessage),
		};

	}
}
