// Ignore Spelling: Utils Ateme

namespace Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge
{
	using Skyline.DataMiner.Net;

	/// <summary>
	/// Represents a client for interacting with an Ateme Titan Edge element.
	/// </summary>
	public interface IAtemeTitanEdgeClient
	{
		/// <summary>
		/// Gets the connection to the DataMiner system.
		/// </summary>
		IConnection Connection { get; }

		/// <summary>
		/// Gets the agent ID of the Ateme Titan Edge element.
		/// </summary>
		int AgentId { get; }

		/// <summary>
		/// Gets the element ID of the Ateme Titan Edge element.
		/// </summary>
		int ElementId { get; }

		/// <summary>
		/// Gets the name of the Ateme Titan Edge element.
		/// </summary>
		string ElementName { get; }

		/// <summary>
		/// Sends a bulk message to the Ateme Titan Edge element.
		/// </summary>
		/// <param name="messages">The array of messages to send.</param>
		void SendConfig(IAtemeTitanEdgeConfig config);
	}
}