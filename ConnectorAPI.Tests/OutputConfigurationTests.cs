namespace ConnectorAPI.Tests
{
	using System;
	using FluentAssertions;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;
	using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge;

	[TestClass]
	public class OutputConfigurationTests
	{
		private OutputConfiguration _config;
		private IAtemeTitanEdgeClient _mockClient;

		[TestInitialize]
		public void TestInitialize()
		{
			_config = new OutputConfiguration(1, 2, ConfigType.Decoder);
			var mock = new Mock<IAtemeTitanEdgeClient>();
			mock.Setup(x => x.SendConfig(It.IsAny<IAtemeTitanEdgeConfig>())).Callback<IAtemeTitanEdgeConfig>(config => config.ToInterAppMessages());
			_mockClient = mock.Object;
		}

		[TestMethod]
		public void ChangeType_ShouldAddMessage()
		{
			_config.ChangeType(SdiType.HdSdi);
			var messages = _config.ToInterAppMessages();
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<OutputConfiguration.OutputConnectorTypeMessage>();
		}

		[TestMethod]
		public void SetName_ShouldAddMessage()
		{
			_config.SetName("Test Output");
			var messages = _config.ToInterAppMessages();
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<OutputConfiguration.OutputConnectorNameMessage>();
		}

		[TestMethod]
		public void EnableColorimetry_ShouldAddMessage()
		{
			_config.EnableColorimetry(true);
			var messages = _config.ToInterAppMessages();
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<OutputConfiguration.OutputConnectorColorimetryMessage>();
		}

		[TestMethod]
		public void SetColorimetryConversion_ShouldAddMessage()
		{
			_config.SetColorimetryConversion(ColorimetryConversion.Hdr10);
			var messages = _config.ToInterAppMessages();
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<OutputConfiguration.OutputConnectorColorimetryConversionMessage>();
		}

		[TestMethod]
		public void SetIutName_ShouldAddMessage()
		{
			_config.SetIutName(IutName.Hable);
			var messages = _config.ToInterAppMessages();
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<OutputConfiguration.OutputConnectorIutMessage>();
		}

		[TestMethod]
		public void ToInterAppMessages_NoMessages_ShouldThrow()
		{
			var act = () => _config.ToInterAppMessages();
			act.Should().Throw<InvalidOperationException>();
		}

		[TestMethod]
		public void Send_NoMessages_ShouldThrow()
		{
			var act = () => _config.Send(_mockClient);
			act.Should().Throw<InvalidOperationException>();
		}

		[TestMethod]
		public void Send_NullClient_ShouldThrow()
		{
			_config.ChangeType();
			var act = () => _config.Send(null);
			act.Should().Throw<ArgumentNullException>();
		}

		[TestMethod]
		public void Builder_Chaining_ShouldAddAllMessages()
		{
			_config.ChangeType(SdiType.HdSdi)
				.SetName("Output1")
				.EnableColorimetry(true)
				.SetColorimetryConversion(ColorimetryConversion.Hdr10)
				.SetIutName(IutName.Hable);
			var messages = _config.ToInterAppMessages();
			messages.Should().HaveCount(5);
			messages[0].Should().BeOfType<OutputConfiguration.OutputConnectorTypeMessage>();
			messages[1].Should().BeOfType<OutputConfiguration.OutputConnectorNameMessage>();
			messages[2].Should().BeOfType<OutputConfiguration.OutputConnectorColorimetryMessage>();
			messages[3].Should().BeOfType<OutputConfiguration.OutputConnectorColorimetryConversionMessage>();
			messages[4].Should().BeOfType<OutputConfiguration.OutputConnectorIutMessage>();
		}
	}
}
