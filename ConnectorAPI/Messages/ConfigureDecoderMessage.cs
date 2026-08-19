namespace Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge.Messages
{
	using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

	/// <summary>
	/// Generic PID-based passthrough message that instructs the Ateme Titan Edge
	/// connector to write <see cref="Value"/> to the parameter identified by
	/// <see cref="Pid"/> on the decoder side. For table parameters, either
	/// <see cref="PrimaryKey"/> or <see cref="DisplayKey"/> identifies the row.
	/// </summary>
	public class ConfigureDecoderMessage : Message
	{
		/// <summary>
		/// Gets or sets the write parameter ID on the connector.
		/// </summary>
		public int Pid { get; set; }

		/// <summary>
		/// Gets or sets the serialized value to write.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets the primary key of the target table row.
		/// Null for scalar parameters or when only <see cref="DisplayKey"/> is known.
		/// </summary>
		public string PrimaryKey { get; set; }

		/// <summary>
		/// Gets or sets the display key of the target table row.
		/// Null for scalar parameters or when only <see cref="PrimaryKey"/> is known.
		/// </summary>
		public string DisplayKey { get; set; }
	}
}
