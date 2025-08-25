using System;
using System.Collections.Generic;
using System.Text;
using Skyline.DataMiner.Core.InterAppCalls.Common.CallBulk;
using Skyline.DataMiner.Core.InterAppCalls.Common.Shared;
using Skyline.DataMiner.Scripting;

namespace Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge
{
	/// <summary>
	///     Ateme Titan Edge Server instance to handle InterApp messages.
	/// </summary>
	public class AtemeTitanEdgeServer
	{
		private readonly List<Type> knownTypes = AtemeTitanEdgeKnownTypes.KnownTypes;

		private readonly SLProtocol protocol;

		/// <summary>
		///     Evs Cerebrum Server instance to handle InterApp messages.
		/// </summary>
		/// <param name="protocol">SLProtocol interface allowing communication with the SLProtocol process.</param>
		public AtemeTitanEdgeServer(SLProtocol protocol)
		{
			this.protocol = protocol;
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
		///     Handles the request based on the attached messages to executor mapping.
		/// </summary>
		/// <param name="rawData">The serialized raw data.</param>
		public void HandleRequest(string rawData)
		{
			var receivedCall = InterAppCallFactory.CreateFromRaw(rawData, knownTypes);

			foreach (var request in receivedCall.Messages)
			{
				request.TryExecute(protocol, protocol, MessageToExecutorMapping, out var response);

				if (response == null)
				{
					continue;
				}

				request.Reply(
					protocol.SLNet.RawConnection,
					response,
					knownTypes);
			}
		}
	}
}
