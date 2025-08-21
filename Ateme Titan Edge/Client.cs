using System;
using System.Collections.Generic;
using System.Security.Authentication;
using Skyline.DataMiner.Core.DataMinerSystem.Common;
using Skyline.DataMiner.Core.DataMinerSystem.Protocol;
using Skyline.DataMiner.Core.InterAppCalls.Common.CallBulk;
using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;
using Skyline.DataMiner.Core.InterAppCalls.Common.Shared;
using Skyline.DataMiner.Scripting;

namespace Skyline.DataMiner.Utils.AtemeTitanEdge
{
	public class Client
	{
		private readonly List<Type> knownTypes = AtemeTitanEdgeKnownTypes.KnownTypes;

		private readonly SLProtocol protocol;

		private readonly int receiverPid;

		private readonly DmsElementId remoteElementId;

		protected Client(SLProtocol protocol)
		{
			this.protocol = protocol;
		}

		protected Client(SLProtocol protocol, DmsElementId remoteElementId, int receiverPid) : this(protocol)
		{
			this.remoteElementId = remoteElementId;
			this.receiverPid = receiverPid;
		}

		private IDictionary<Type, Type> MessageToExecutorMapping { get; } = new Dictionary<Type, Type>();

		/// <summary>
		///     A mapping to link message type with the right execution type.
		/// </summary>
		/// <param name="message">Message type.</param>
		/// <param name="executor">Executor type.</param>
		public void AttachMessageToExecutor(Type message, Type executor)
		{
			MessageToExecutorMapping[message] = executor;
		}

		/// <summary>
		///     Handles the response based on the attached messages to executor mapping.
		/// </summary>
		/// <param name="rawData">The serialized raw data.</param>
		public void HandleResponse(string rawData)
		{
			var receivedCall = InterAppCallFactory.CreateFromRaw(rawData, knownTypes);

			foreach (var request in receivedCall.Messages)
			{
				request.TryExecute(protocol, protocol, MessageToExecutorMapping, out var response);

				if (response == null)
				{
					continue;
				}

				protocol.Log(
					$"QA{protocol.QActionID}|HandleResponse|Sending return message '{response}' with GUID '{response.Guid}'.",
					LogType.DebugInfo,
					LogLevel.NoLogging);

				request.Reply(
					protocol.SLNet.RawConnection,
					response,
					knownTypes);
			}
		}

		/// <summary>
		///     Sends this call via SLNet and waits until timeout for a single reply for each message.
		/// </summary>
		/// <param name="timeout">Maximum time to wait between received replies. Resets each time a reply is received.</param>
		/// <param name="message">Represents a single command or response.</param>
		/// <returns>IEnumerable&lt;Message&gt;</returns>
		public IEnumerable<Message> SendRequest(TimeSpan timeout, Message message)
		{
			var element = protocol.GetDms().GetElement(remoteElementId);
			var interAppCall = InterAppCallFactory.CreateNew();
			interAppCall.Messages.Add(message);
			interAppCall.Source = new Source(protocol.ElementName, protocol.DataMinerID, protocol.ElementID);
			interAppCall.ReturnAddress = new ReturnAddress(element.AgentId, element.Id, 9000001);

			return interAppCall.Send(protocol.SLNet.RawConnection, element.AgentId, element.Id, 9000000, timeout, knownTypes);
		}

		/// <summary>
		///     Sends this call via SLNet without waiting on a reply.
		/// </summary>
		/// <param name="messages">Represents a single command or response.</param>
		protected void SendBulkMessage(Message[] messages)
		{
			var element = protocol.GetDms().GetElement(remoteElementId);

			if (element.State != ElementState.Active)
			{
				protocol.Log(
					$"QA{protocol.QActionID}|SendMessage|{element.Name} is not active. Current state: {element.State}",
					LogType.DebugInfo,
					LogLevel.NoLogging);

				return;
			}

			var interAppCall = InterAppCallFactory.CreateNew();
			interAppCall.Messages.AddMessage(messages);
			interAppCall.Source = new Source(protocol.ElementName, protocol.DataMinerID, protocol.ElementID);
			interAppCall.ReturnAddress = new ReturnAddress(protocol.DataMinerID, protocol.ElementID, receiverPid);
			interAppCall.Send(protocol.SLNet.RawConnection, element.AgentId, element.Id, 9000000, knownTypes);
		}
	}
}