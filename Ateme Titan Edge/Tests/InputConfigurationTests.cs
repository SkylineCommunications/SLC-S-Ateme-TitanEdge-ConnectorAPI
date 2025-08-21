using NUnit.Framework;
using System;
using Skyline.DataMiner.Utils.AtemeTitanEdge;
using Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Common.Enums;
using Skyline.DataMiner.Scripting;
using Moq;
using System.Linq;

namespace Skyline.DataMiner.Utils.AtemeTitanEdge.Tests
{
	[TestFixture]
	public class InputConfigurationTests
	{
		private AtemeTitanEdgeClient.InputConfiguration _config;
		private Mock<AtemeTitanEdgeClient> _mockClient;

		[SetUp]
		public void SetUp()
		{
			_config = new AtemeTitanEdgeClient.InputConfiguration(1, 2, ConfigType.Decoder);
			_mockClient = new Mock<AtemeTitanEdgeClient>(Mock.Of<SLProtocol>());
		}

		[Test]
		public void EnableInput_ShouldAddMessage()
		{
			_config.EnableInput(true);

			var messagesField = typeof(AtemeTitanEdgeClient.InputConfiguration)
				.GetField("_messages", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

			var messages = (System.Collections.Generic.List<object>)messagesField.GetValue(_config);

			Assert.That(messages.Count, Is.EqualTo(1));
			Assert.That(messages.First().GetType().Name, Is.EqualTo("EnableInputMessage"));
		}

		[Test]
		public void SetIpAddress_Valid_ShouldAddMessage()
		{
			_config.SetIpAddress("192.168.1.100");

			var messagesField = typeof(AtemeTitanEdgeClient.InputConfiguration)
				.GetField("_messages", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

			var messages = (System.Collections.Generic.List<object>)messagesField.GetValue(_config);

			Assert.That(messages.Count, Is.EqualTo(1));
			Assert.That(messages.First().GetType().Name, Is.EqualTo("SetIpAddressMessage"));
		}

		[Test]
		public void SetIpAddress_Invalid_ShouldThrow()
		{
			Assert.Throws<ArgumentException>(() => _config.SetIpAddress("999.999.999.999"));
		}

		[Test]
		public void SetIpPort_OutOfRange_ShouldThrow()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => _config.SetIpPort(70000));
		}

		[Test]
		public void SetBufferDuration_Negative_ShouldThrow()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => _config.SetBufferDuration(-1));
		}

		[Test]
		public void Send_NoMessages_ShouldThrow()
		{
			Assert.Throws<InvalidOperationException>(() => _config.Send(_mockClient.Object));
		}
	}
}
