namespace ConnectorAPI.Tests;

using System;
using System.Collections.Generic;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge;
using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge.Messages;
using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;
using Skyline.DataMiner.Net;
using Skyline.DataMiner.Net.Messages;

[TestClass]
public class AtemeTitanEdgeClientTests
{
	[TestMethod]
	public void SendBulk_NullMessages_ThrowsArgumentNullException()
	{
		var client = CreateClient();

		Action act = () => client.SendBulk(null);

		act.Should().Throw<ArgumentNullException>();
	}

	[TestMethod]
	public void SendBulk_EmptyMessages_DoesNotThrow()
	{
		var client = CreateClient();

		Action act = () => client.SendBulk(Array.Empty<Message>());

		act.Should().NotThrow();
	}

	[TestMethod]
	public void SendBulk_EmptyList_DoesNotThrow()
	{
		var client = CreateClient();
		var messages = new List<Message>();

		Action act = () => client.SendBulk(messages);

		act.Should().NotThrow();
	}

	[TestMethod]
	public void SendBulk_EmptyEnumerable_DoesNotThrow()
	{
		var client = CreateClient();

		Action act = () => client.SendBulk(EmptyMessageSequence());

		act.Should().NotThrow();
	}

	private static IEnumerable<Message> EmptyMessageSequence()
	{
		yield break;
	}

	private static AtemeTitanEdgeClient CreateClient()
	{
		var elementInfo = new ElementInfoEventMessage
		{
			DataMinerID = 123,
			ElementID = 456,
			Name = "TitanEdge-01",
			Protocol = "Ateme Titan Edge",
		};

		var connection = new Mock<IConnection>(MockBehavior.Loose);
		connection
			.Setup(c => c.HandleSingleResponseMessage(It.IsAny<GetElementByNameMessage>()))
			.Returns(elementInfo);

		return new AtemeTitanEdgeClient(connection.Object, elementInfo.Name);
	}
}
