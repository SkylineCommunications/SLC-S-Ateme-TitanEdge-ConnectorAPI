// Ignore Spelling: Utils Ateme

namespace Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

	using Skyline.DataMiner.Core.InterAppCalls.Common.CallBulk;
	using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;
	using Skyline.DataMiner.Core.InterAppCalls.Common.Shared;
	using Skyline.DataMiner.Net;

	/// <summary>
	/// Represents a client for interacting with the Ateme Titan Edge system.
	/// Provides methods to create and send inter-app messages to a DataMiner instance.
	/// </summary>
	public class AtemeTitanEdgeClient : IAtemeTitanEdgeClient
	{
		private const int IacReceiverPID = 9_000_000;
		private const int IacResponsePID = 9_000_001;
		private const string ProtocolName = "Ateme Titan Edge";

		/// <summary>
		/// Initializes a new instance of the <see cref="AtemeTitanEdgeClient"/> class
		/// using the specified protocol.
		/// </summary>
		/// <param name="connection">The <see cref="IConnection"/> instance that allows communication with the DataMiner system.</param>
		/// <param name="agentId">The ID of the DataMiner agent hosting the remote element.</param>
		/// <param name="elementId">The ID of the remote element.</param>
		public AtemeTitanEdgeClient(IConnection connection, int agentId, int elementId)
		{
			if (agentId <= 0)
				throw new ArgumentOutOfRangeException(nameof(agentId), "Agent ID must be greater than zero.");
			if (elementId <= 0)
				throw new ArgumentOutOfRangeException(nameof(elementId), "Element ID must be greater than zero.");

			Connection = connection ?? throw new ArgumentNullException(nameof(connection), "Connection cannot be null.");

			Net.Messages.ElementInfoEventMessage elementInfo;
			try
			{
				elementInfo = (Net.Messages.ElementInfoEventMessage)Connection.HandleSingleResponseMessage(new Net.Messages.GetElementByIDMessage
				{
					DataMinerID = agentId,
					ElementID = elementId,
				});
			}
			catch
			{
				throw new ArgumentException($"The element does not exists with id '{agentId}/{elementId}'", nameof(elementId));
			}

			if (elementInfo.Protocol != ProtocolName)
			{
				throw new ArgumentException($"The element is not running protocol '{ProtocolName}'", nameof(elementId));
			}

			AgentId = elementInfo.DataMinerID;
			ElementId = elementInfo.ElementID;
			ElementName = elementInfo.Name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AtemeTitanEdgeClient"/> class
		/// using the specified protocol, remote element ID, and optional receiver PID.
		/// </summary>
		/// <param name="connection">
		/// The <see cref="IConnection"/> instance that allows communication with the DataMiner system.
		/// </param>
		/// <param name="elementName">
		/// The name of the element representing the remote DataMiner element.
		/// </param>
		public AtemeTitanEdgeClient(IConnection connection, string elementName)
		{
			if (String.IsNullOrEmpty(elementName))
			{
				throw new ArgumentException("Please provide a valid Element name.", nameof(elementName));
			}

			Connection = connection ?? throw new ArgumentNullException(nameof(connection));

			Net.Messages.ElementInfoEventMessage elementInfo;
			try
			{
				elementInfo = (Net.Messages.ElementInfoEventMessage)Connection.HandleSingleResponseMessage(new Net.Messages.GetElementByNameMessage
				{
					ElementName = elementName,
				});
			}
			catch (Exception)
			{
				throw new ArgumentException($"The element does not exists with name '{elementName}'", nameof(elementName));
			}

			if (elementInfo.Protocol != ProtocolName)
			{
				throw new ArgumentException($"The element is not running protocol '{ProtocolName}'", nameof(elementName));
			}

			AgentId = elementInfo.DataMinerID;
			ElementId = elementInfo.ElementID;
			ElementName = elementInfo.Name;
		}

		/// <inheritdoc />
		public IConnection Connection { get; }

		/// <inheritdoc />
		public int AgentId { get; }

		/// <inheritdoc />
		public int ElementId { get; }

		/// <inheritdoc />
		public string ElementName { get; }

		/// <inheritdoc />
		public void SendConfig(IAtemeTitanEdgeConfig config)
		{
			var messages = config.ToInterAppMessages();
			SendBulkMessage(messages);
		}

		/// <summary>
		///     Sends this call via SLNet without waiting on a reply.
		/// </summary>
		/// <param name="message">A single InterApp message to send.</param>
		private void SendMessage(Message message)
		{
			var interAppCall = InterAppCallFactory.CreateNew();
			interAppCall.Messages.AddMessage(message);
			interAppCall.ReturnAddress = new ReturnAddress(AgentId, ElementId, IacResponsePID);
			interAppCall.Send(Connection, AgentId, ElementId, IacReceiverPID, AtemeTitanEdgeKnownTypes.KnownTypes);
		}

		/// <summary>
		///     Sends this call via SLNet without waiting on a reply.
		/// </summary>
		/// <param name="messages">The list of InterApp messages to send.</param>
		private void SendBulkMessage(Message[] messages)
		{
			var interAppCall = InterAppCallFactory.CreateNew();
			interAppCall.Messages.AddMessage(messages);
			interAppCall.ReturnAddress = new ReturnAddress(AgentId, ElementId, IacResponsePID);
			interAppCall.Send(Connection, AgentId, ElementId, IacReceiverPID, AtemeTitanEdgeKnownTypes.KnownTypes);
		}
	}
}
