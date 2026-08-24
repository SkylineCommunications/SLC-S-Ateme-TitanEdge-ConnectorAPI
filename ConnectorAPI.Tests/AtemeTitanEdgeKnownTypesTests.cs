namespace ConnectorAPI.Tests;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge;
using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge.Messages;

[TestClass]
public class AtemeTitanEdgeKnownTypesTests
{
	[TestMethod]
	public void KnownTypes_ShouldContainAllSupportedMessages()
	{
		var knownTypes = AtemeTitanEdgeKnownTypes.KnownTypes;

		knownTypes.Should().Contain(typeof(ConfigureDecoderMessage));
		knownTypes.Should().Contain(typeof(ConfigureEncoderMessage));
		knownTypes.Should().Contain(typeof(ConfigureDemodulatorMessage));
		knownTypes.Should().HaveCount(3);
	}
}
