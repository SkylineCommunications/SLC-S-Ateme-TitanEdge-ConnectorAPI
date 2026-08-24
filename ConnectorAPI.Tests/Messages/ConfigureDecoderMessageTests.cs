namespace ConnectorAPI.Tests.Messages;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge.Messages;

[TestClass]
public class ConfigureDecoderMessageTests
{
	[TestMethod]
	public void ConfigureDecoderMessage_ShouldStoreAssignedValues()
	{
		var message = new ConfigureDecoderMessage
		{
			Pid = 1101,
			Value = "enabled",
			PrimaryKey = "decoder-1",
		};

		message.Pid.Should().Be(1101);
		message.Value.Should().Be("enabled");
		message.PrimaryKey.Should().Be("decoder-1");
	}
}
