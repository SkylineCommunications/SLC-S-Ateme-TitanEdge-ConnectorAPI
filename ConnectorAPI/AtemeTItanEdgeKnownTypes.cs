namespace Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge
{
	using System;
	using System.Collections.Generic;

	using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge.Messages;

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
			typeof(ConfigureDecoderMessage),
			typeof(ConfigureEncoderMessage),
			typeof(ConfigureDemodulatorMessage),
		};
	}
}
