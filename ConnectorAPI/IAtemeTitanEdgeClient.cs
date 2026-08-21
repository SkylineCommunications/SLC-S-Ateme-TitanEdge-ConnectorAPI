namespace Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge
{
	using System.Collections.Generic;

	using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;
	using Skyline.DataMiner.Net;

	/// <summary>
	/// Represents a client for interacting with an Ateme Titan Edge element via InterApp calls.
	/// </summary>
	public interface IAtemeTitanEdgeClient
	{
		/// <summary>Gets the connection to the DataMiner system.</summary>
		IConnection Connection { get; }

		/// <summary>Gets the agent ID of the Ateme Titan Edge element.</summary>
		int AgentId { get; }

		/// <summary>Gets the element ID of the Ateme Titan Edge element.</summary>
		int ElementId { get; }

		/// <summary>Gets the name of the Ateme Titan Edge element.</summary>
		string ElementName { get; }

		/// <summary>
		/// Sends the specified InterApp messages to the element in a single bulk call. Fire-and-forget.
		/// </summary>
		/// <param name="messages">The messages to send.</param>
		void SendBulk(IEnumerable<Message> messages);
	}
}