namespace Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge
{
	using System;
	using System.Collections.Generic;

	using static Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge.InputConfiguration;
	using static Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge.OutputConfiguration;

	/// <summary>
	/// Provides a list of known types used in the Ateme Titan Edge connector.
	/// </summary>
	/// <remarks>
	/// This class is used to supply a list of types that are required for serialization and deserialization
	/// of configuration messages in the Ateme Titan Edge connector.
	/// </remarks>
	public static class AtemeTitanEdgeKnownTypes
	{
		/// <summary>
		/// Gets the list of known types for the Ateme Titan Edge configuration.
		/// </summary>
		/// <remarks>
		/// This list is used for serialization and deserialization purposes.
		/// </remarks>
		/// <value>
		/// A list of <see cref="Type"/> objects representing the known types.
		/// </value>
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
