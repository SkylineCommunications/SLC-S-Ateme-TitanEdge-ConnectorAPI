namespace ConnectorAPI.Tests
{
	using System;
	using FluentAssertions;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;
	using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge;

	[TestClass]
	public class InputConfigurationTests
	{
		private InputConfiguration _config;
		private IAtemeTitanEdgeClient _mockClient;

		[TestInitialize]
		public void TestInitialize()
		{
			_config = new InputConfiguration(1, 2, ConfigType.Decoder);
			var mock = new Mock<IAtemeTitanEdgeClient>();
			mock.Setup(x => x.SendConfig(It.IsAny<IAtemeTitanEdgeConfig>())).Callback<IAtemeTitanEdgeConfig>(config => config.ToInterAppMessages());
			_mockClient = mock.Object;
		}

		[TestMethod]
		public void EnableInput_ShouldAddMessage()
		{
			// Arrange
			_config.EnableInput(true);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.EnableInputMessage>();
		}

		[TestMethod]
		public void SetIpAddress_Valid_ShouldAddMessage()
		{
			// Arrange
			_config.SetIpAddress("192.168.1.100");

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.SetIpAddressMessage>();
		}

		[TestMethod]
		public void SetIpAddress_Invalid_ShouldThrow()
		{
			var act = () => _config.SetIpAddress("999.999.999.999");

			act.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void SetInputType_ShouldAddMessage()
		{
			// Arrange
			_config.SetInputType(InputType.Ip);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.SetInputTypeMessage>();
		}

		[TestMethod]
		public void SetInputType_Invalid_ShouldThrow()
		{
			var act = () => _config.SetInputType((InputType)999);

			act.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void SetIpPort_Valid_ShouldAddMessage()
		{
			// Arrange
			_config.SetIpPort(1234);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.SetIpPortMessage>();
		}

		[TestMethod]
		public void SetIpPort_OutOfRange_ShouldThrow()
		{
			var act = () => _config.SetIpPort(70000);

			act.Should().Throw<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void SetInputInterface_ShouldAddMessage()
		{
			// Arrange
			_config.SetInputInterface("eth0");

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.SetInterfaceMessage>();
		}

		[TestMethod]
		public void EnableInputFec_ShouldAddMessage()
		{
			// Arrange
			_config.EnableInputFec(true);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.EnableInputFecMessage>();
		}

		[TestMethod]
		public void SetBufferDuration_Valid_ShouldAddMessage()
		{
			// Arrange
			_config.SetBufferDuration(100);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.SetBufferDurationMessage>();
		}

		[TestMethod]
		public void SetBufferDuration_Negative_ShouldThrow()
		{
			var act = () => _config.SetBufferDuration(-1);

			act.Should().Throw<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void EnableSourceSpecificMulticast_ShouldAddMessage()
		{
			// Arrange
			_config.EnableSourceSpecificMulticast(true);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.EnableSourceSpecificMulticastMessage>();
		}

		[TestMethod]
		public void SetSourceSpecificMulticastMode_ShouldAddMessage()
		{
			// Arrange
			_config.SetSourceSpecificMulticastMode(SourceSpecificMulticastMode.Include);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.SetSourceSpecificMulticastModeMessage>();
		}

		[TestMethod]
		public void EnableSMPTE2022_7_ShouldAddMessage()
		{
			// Arrange
			_config.EnableSMPTE2022_7(true);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.EnableSmpte20227Message>();
		}

		[TestMethod]
		public void SetSMPTE2022_7IpAddress_Valid_ShouldAddMessage()
		{
			// Arrange
			_config.SetSMPTE2022_7IpAddress("10.0.0.1");

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.SetSmpte20227IpAddressMessage>();
		}

		[TestMethod]
		public void SetSMPTE2022_7IpAddress_Invalid_ShouldThrow()
		{
			var act = () => _config.SetSMPTE2022_7IpAddress("bad_ip");

			act.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void SetSMPTE2022_7Port_Valid_ShouldAddMessage()
		{
			// Arrange
			_config.SetSMPTE2022_7Port(1234);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.SetSmpte20227PortMessage>();
		}

		[TestMethod]
		public void SetSMPTE2022_7Port_OutOfRange_ShouldThrow()
		{
			var act = () => _config.SetSMPTE2022_7Port(70000);

			act.Should().Throw<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void SetSMPTE2022_7Interface_ShouldAddMessage()
		{
			// Arrange
			_config.SetSMPTE2022_7Interface("eth1");

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.SetSmpte20227InterfaceMessage>();
		}

		[TestMethod]
		public void SetSMPTE2022_7Skew_ShouldAddMessage()
		{
			// Arrange
			_config.SetSMPTE2022_7Skew(SmpteSkew.High);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.SetSmpte20227SkewMessage>();
		}

		[TestMethod]
		public void SetSMPTE2022_7CustomSkew_Valid_ShouldAddMessage()
		{
			// Arrange
			_config.SetSMPTE2022_7CustomSkew(123);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.SetSmpte20227CustomSkewMessage>();
		}

		[TestMethod]
		public void SetSMPTE2022_7CustomSkew_Negative_ShouldThrow()
		{
			var act = () => _config.SetSMPTE2022_7CustomSkew(-1);

			act.Should().Throw<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void EnableInputVbr_ShouldAddMessage()
		{
			// Arrange
			_config.EnableInputVbr(true);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.EnableInputVbrMessage>();
		}

		[TestMethod]
		public void EnableLowLatencyFec_ShouldAddMessage()
		{
			// Arrange
			_config.EnableLowLatencyFec(true);

			// Act
			var messages = _config.ToInterAppMessages();

			// Assert
			messages.Should().HaveCount(1);
			messages[0].Should().BeOfType<InputConfiguration.EnableLowLatencyFecMessage>();
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
			_config.EnableInput();
			var act = () => _config.Send(null);

			act.Should().Throw<ArgumentNullException>();
		}

		[TestMethod]
		public void Builder_Chaining_ShouldAddAllMessages()
		{
			_config.EnableInput(true)
				.SetIpAddress("192.168.1.1")
				.SetInputType(InputType.Ip)
				.SetIpPort(1234)
				.SetInputInterface("eth0")
				.EnableInputFec(true)
				.SetBufferDuration(100)
				.EnableSourceSpecificMulticast(true)
				.SetSourceSpecificMulticastMode(SourceSpecificMulticastMode.Include)
				.EnableSMPTE2022_7(true)
				.SetSMPTE2022_7IpAddress("10.0.0.1")
				.SetSMPTE2022_7Port(1234)
				.SetSMPTE2022_7Interface("eth1")
				.SetSMPTE2022_7Skew(SmpteSkew.High)
				.SetSMPTE2022_7CustomSkew(123)
				.EnableInputVbr(true)
				.EnableLowLatencyFec(true);
			var messages = _config.ToInterAppMessages();
			messages.Should().HaveCount(17);
		}
	}
}