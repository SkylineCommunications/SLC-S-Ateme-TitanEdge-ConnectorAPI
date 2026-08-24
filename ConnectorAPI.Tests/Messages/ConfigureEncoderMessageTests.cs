namespace ConnectorAPI.Tests.Messages;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge.Messages;

[TestClass]
public class ConfigureEncoderMessageTests
{
	[TestMethod]
	public void ConfigureEncoderMessage_ShouldStoreAssignedValues()
	{
		var message = new ConfigureEncoderMessage
		{
			Pid = 2201,
			Value = "srt",
			PrimaryKey = "encoder-1",
		};

		message.Pid.Should().Be(2201);
		message.Value.Should().Be("srt");
		message.PrimaryKey.Should().Be("encoder-1");
	}
}
