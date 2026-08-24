namespace ConnectorAPI.Tests.Messages;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge.Messages;

[TestClass]
public class ConfigureDemodulatorMessageTests
{
	[TestMethod]
	public void ConfigureDemodulatorMessage_ShouldStoreAssignedValues()
	{
		var message = new ConfigureDemodulatorMessage
		{
			Pid = 3301,
			Value = "27500",
			PrimaryKey = "demod-1",
		};

		message.Pid.Should().Be(3301);
		message.Value.Should().Be("27500");
		message.PrimaryKey.Should().Be("demod-1");
	}
}
