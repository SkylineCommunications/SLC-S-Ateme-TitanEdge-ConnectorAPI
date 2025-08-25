namespace Skyline.DataMiner.Utils.AtemeTitanEdge
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

	using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

	/// <summary>
	/// Builder class for constructing and sending input configuration messages for a specific channel and input.
	/// <para>
	/// This class provides a fluent API to configure various input parameters such as IP address, port, interface, FEC, buffer duration, multicast options, SMPTE 2022-7 settings, and more.
	/// </para>
	/// <example>
	/// <code>
	/// var config = new InputConfiguration(0, 1, ConfigType.Encoder)
	///     .EnableInput(true)
	///     .SetIpAddress("239.0.0.1")
	///     .SetIpPort(5000)
	///     .EnableInputFec(true);
	/// config.Send(client);
	/// </code>
	/// </example>
	/// <remarks>
	/// Use the <see cref="Send(IAtemeTitanEdgeClient)"/> method to send all configured messages at once.
	/// </remarks>
	/// </summary>
	public sealed class InputConfiguration : IAtemeTitanEdgeConfig
	{
		private readonly int _inputIndex;
		private readonly int _channelIndex;
		private readonly ConfigType _type;
		private readonly List<Message> _messages = new List<Message>();

		/// <summary>
		/// Initializes a new instance of the <see cref="InputConfiguration"/> class for the specified input and channel.
		/// </summary>
		/// <param name="inputIndex">The input index of the channel.</param>
		/// <param name="channelIndex">The channel index.</param>
		/// <param name="type">The type of the channel.</param>
		public InputConfiguration(int inputIndex, int channelIndex, ConfigType type)
		{
			_inputIndex = inputIndex;
			_channelIndex = channelIndex;
			_type = type;
		}

		/// <summary>
		/// Enables or disables the input.
		/// </summary>
		/// <param name="isEnabled">True to enable the input, false to disable. Defaults to false.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		public InputConfiguration EnableInput(bool isEnabled = false)
		{
			_messages.Add(new EnableInputMessage(_channelIndex, _inputIndex, isEnabled, _type));
			return this;
		}

		/// <summary>
		/// Sets the IPv4 address for the input.
		/// </summary>
		/// <param name="ipAddress">The IPv4 address to set. Must be in valid format (e.g., "192.168.1.1").</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		/// <exception cref="ArgumentException">Thrown if <paramref name="ipAddress"/> is null, empty, or invalid.</exception>
		public InputConfiguration SetIpAddress(string ipAddress)
		{
			if (string.IsNullOrWhiteSpace(ipAddress))
				throw new ArgumentException("IP address cannot be null or empty.", nameof(ipAddress));

			string pattern = @"^((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.){3}(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)$";
			if (!Regex.IsMatch(ipAddress, pattern))
				throw new ArgumentException("Invalid IPv4 address format.", nameof(ipAddress));

			_messages.Add(new SetIpAddressMessage(_channelIndex, _inputIndex, ipAddress, _type));
			return this;
		}

		/// <summary>
		/// Sets the input type for the channel/input.
		/// </summary>
		/// <param name="inputType">The type of input to set, as an <see cref="InputType"/> enum.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		public InputConfiguration SetInputType(InputType inputType)
		{
			if (!Enum.IsDefined(typeof(InputType), inputType))
				throw new ArgumentException("Invalid input type.", nameof(inputType));

			// Convert enum to string for the message
			string typeString = inputType.ToApiString();

			_messages.Add(new SetInputTypeMessage(_channelIndex, _inputIndex, typeString, _type));
			return this;
		}

		/// <summary>
		/// Sets the IP port for the channel/input.
		/// </summary>
		/// <param name="port">The port number to set. Must be between 1 and 65535. Defaults to 1234.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="port"/> is outside 1-65535.</exception>
		public InputConfiguration SetIpPort(int port = 1234)
		{
			if (port < 1 || port > 65535)
				throw new ArgumentOutOfRangeException(nameof(port), "Port must be between 1 and 65535.");

			_messages.Add(new SetIpPortMessage(_channelIndex, _inputIndex, port, _type));
			return this;
		}

		/// <summary>
		/// Sets the interface for the channel/input.
		/// </summary>
		/// <param name="interfaceName">The interface name to set.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		public InputConfiguration SetInputInterface(string interfaceName)
		{
			_messages.Add(new SetInterfaceMessage(_channelIndex, _inputIndex, interfaceName, _type));
			return this;
		}

		/// <summary>
		/// Enables or disables FEC (Forward Error Correction) for the input.
		/// </summary>
		/// <param name="isEnabled">True to enable FEC, false to disable. Defaults to false.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		public InputConfiguration EnableInputFec(bool isEnabled = false)
		{
			_messages.Add(new EnableInputFecMessage(_channelIndex, _inputIndex, isEnabled, _type));
			return this;
		}

		/// <summary>
		/// Sets the buffer duration (in milliseconds) for the input.
		/// </summary>
		/// <param name="durationMs">Buffer duration in milliseconds. Must be 0 or greater. Defaults to 50.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="durationMs"/> is less than 0.</exception>
		public InputConfiguration SetBufferDuration(int durationMs = 50)
		{
			if (durationMs < 0)
				throw new ArgumentOutOfRangeException(nameof(durationMs), "Buffer duration must be 0 or greater.");

			_messages.Add(new SetBufferDurationMessage(_channelIndex, _inputIndex, durationMs, _type));
			return this;
		}

		/// <summary>
		/// Enables or disables Source-Specific Multicast (SSM) for the input.
		/// </summary>
		/// <param name="isEnabled">True to enable SSM, false to disable. Defaults to false.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		public InputConfiguration EnableSourceSpecificMulticast(bool isEnabled = false)
		{
			_messages.Add(new EnableSourceSpecificMulticastMessage(_channelIndex, _inputIndex, isEnabled, _type));
			return this;
		}

		/// <summary>
		/// Sets the Source-Specific Multicast (SSM) mode for the input.
		/// </summary>
		/// <param name="mode">The SSM mode to set.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		public InputConfiguration SetSourceSpecificMulticastMode(SourceSpecificMulticastMode mode)
		{
			string modeString = mode.ToString().ToLowerInvariant();
			_messages.Add(new SetSourceSpecificMulticastModeMessage(_channelIndex, _inputIndex, modeString, _type));
			return this;
		}

		/// <summary>
		/// Enables or disables SMPTE 2022-7 for the input.
		/// </summary>
		/// <param name="isEnabled">True to enable SMPTE 2022-7, false to disable. Defaults to false.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		public InputConfiguration EnableSMPTE2022_7(bool isEnabled = false)
		{
			_messages.Add(new EnableSmpte20227Message(_channelIndex, _inputIndex, isEnabled, _type));
			return this;
		}

		/// <summary>
		/// Sets the SMPTE IP address for the input.
		/// Defaults to "0.0.0.0" if not specified.
		/// </summary>
		/// <param name="ipAddress">The SMPTE IP address to set. Optional, defaults to "0.0.0.0".</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		/// <exception cref="ArgumentException">Thrown if IP is null, empty, or invalid.</exception>
		public InputConfiguration SetSMPTE2022_7IpAddress(string ipAddress = "0.0.0.0")
		{
			if (string.IsNullOrWhiteSpace(ipAddress))
				throw new ArgumentException("SMPTE IP address cannot be null or empty.", nameof(ipAddress));

			string pattern = @"^((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.){3}(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)$";
			if (!Regex.IsMatch(ipAddress, pattern))
				throw new ArgumentException("Invalid IPv4 address format.", nameof(ipAddress));

			_messages.Add(new SetSmpte20227IpAddressMessage(_channelIndex, _inputIndex, ipAddress, _type));
			return this;
		}

		/// <summary>
		/// Sets the SMPTE port for the input. Defaults to 1234 if not specified.
		/// </summary>
		/// <param name="port">The port number to set. Must be between 1 and 65535. Defaults to 1234.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="port"/> is outside 1-65535.</exception>
		public InputConfiguration SetSMPTE2022_7Port(int port = 1234)
		{
			if (port < 1 || port > 65535)
				throw new ArgumentOutOfRangeException(nameof(port), "Port must be between 1 and 65535.");

			_messages.Add(new SetSmpte20227PortMessage(_channelIndex, _inputIndex, port, _type));
			return this;
		}

		/// <summary>
		/// Sets the SMPTE 2022-7 Ethernet interface for the input.
		/// </summary>
		/// <param name="interfaceName">The interface name. Defaults to an empty string.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		public InputConfiguration SetSMPTE2022_7Interface(string interfaceName = "")
		{
			_messages.Add(new SetSmpte20227InterfaceMessage(_channelIndex, _inputIndex, interfaceName, _type));
			return this;
		}

		/// <summary>
		/// Sets the SMPTE 2022-7 network skew for the input.
		/// <para>SmpteSkew values:</para>
		/// <list type="bullet">
		/// <item><description>Low: below 10 ms</description></item>
		/// <item><description>Moderate: below 50 ms</description></item>
		/// <item><description>High: below 450 ms</description></item>
		/// <item><description>Custom: user-defined in customSkew</description></item>
		/// </list>
		/// </summary>
		/// <param name="skew">The skew value to set.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		public InputConfiguration SetSMPTE2022_7Skew(SmpteSkew skew = SmpteSkew.Low)
		{
			string skewString = skew.ToString().ToLowerInvariant();
			_messages.Add(new SetSmpte20227SkewMessage(_channelIndex, _inputIndex, skewString, _type));
			return this;
		}

		/// <summary>
		/// Sets the custom SMPTE 2022-7 network skew in milliseconds. Used when <see cref="SmpteSkew.Custom"/> is selected.
		/// </summary>
		/// <param name="customSkewMs">The custom skew value in milliseconds. Must be 0 or greater. Defaults to 0.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="customSkewMs"/> is less than 0.</exception>
		public InputConfiguration SetSMPTE2022_7CustomSkew(int customSkewMs = 0)
		{
			if (customSkewMs < 0)
				throw new ArgumentOutOfRangeException(nameof(customSkewMs), "Custom skew must be 0 or greater.");

			_messages.Add(new SetSmpte20227CustomSkewMessage(_channelIndex, _inputIndex, customSkewMs, _type));
			return this;
		}

		/// <summary>
		/// Enables or disables TS VBR (Variable Bitrate) stream support for the input.
		/// </summary>
		/// <param name="isEnabled">True to enable VBR support, false to disable. Defaults to false.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		public InputConfiguration EnableInputVbr(bool isEnabled = false)
		{
			_messages.Add(new EnableInputVbrMessage(_channelIndex, _inputIndex, isEnabled, _type));
			return this;
		}

		/// <summary>
		/// Enables or disables the proprietary low-latency FEC implementation for the input.
		/// Uses the CM5000-specific implementation.
		/// </summary>
		/// <param name="isEnabled">True to enable low-latency FEC, false to disable. Defaults to false.</param>
		/// <returns>The current <see cref="InputConfiguration"/> for chaining.</returns>
		public InputConfiguration EnableLowLatencyFec(bool isEnabled = false)
		{
			_messages.Add(new EnableLowLatencyFecMessage(_channelIndex, _inputIndex, isEnabled, _type));
			return this;
		}

		/// <inheritdoc />
		public Message[] ToInterAppMessages()
		{
			if (!_messages.Any())
				throw new InvalidOperationException("No messages to build.");

			return _messages.ToArray();
		}

		/// <summary>
		/// Sends all configured messages to the DataMiner instance using the specified client.
		/// </summary>
		/// <param name="client">The <see cref="AtemeTitanEdgeClient"/> instance used to send messages.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
		/// <exception cref="InvalidOperationException">Thrown if no messages have been added to the builder.</exception>
		public void Send(IAtemeTitanEdgeClient client)
		{
			if (client == null)
				throw new ArgumentNullException(nameof(client), "Client cannot be null.");

			client.SendConfig(this);
		}

		/// <summary>
		/// Private message class, used for enabling/disabling inputs.
		/// </summary>
		public sealed class EnableInputMessage : ConfigBaseMessage
		{
			public bool IsEnabled { get; }

			internal EnableInputMessage(int channelIndex, int inputIndex, bool isEnabled, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				IsEnabled = isEnabled;
			}
		}


		/// <summary>
		/// Private message class, used for setting IP address.
		/// </summary>
		public sealed class SetIpAddressMessage : ConfigBaseMessage
		{
			public string IpAddress { get; }

			internal SetIpAddressMessage(int channelIndex, int inputIndex, string ipAddress, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				IpAddress = ipAddress;
			}
		}


		/// <summary>
		/// Internal message class representing an input type configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class SetInputTypeMessage : ConfigBaseMessage
		{
			public string InputType { get; }

			internal SetInputTypeMessage(int channelIndex, int inputIndex, string inputType, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				InputType = inputType;
			}
		}

		/// <summary>
		/// Internal message class representing an IP port configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class SetIpPortMessage : ConfigBaseMessage
		{
			public int Port { get; }

			internal SetIpPortMessage(int channelIndex, int inputIndex, int port, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				Port = port;
			}
		}


		/// <summary>
		/// Internal message class representing an interface configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class SetInterfaceMessage : ConfigBaseMessage
		{
			public string Interface { get; }

			internal SetInterfaceMessage(int channelIndex, int inputIndex, string interfaceName, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				Interface = interfaceName;
			}
		}


		/// <summary>
		/// Internal message class representing an input FEC configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class EnableInputFecMessage : ConfigBaseMessage
		{
			public bool IsEnabled { get; }

			internal EnableInputFecMessage(int channelIndex, int inputIndex, bool isEnabled, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				IsEnabled = isEnabled;
			}
		}

		/// <summary>
		/// Internal message class representing a buffer duration configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class SetBufferDurationMessage : ConfigBaseMessage
		{
			public int DurationMs { get; }

			internal SetBufferDurationMessage(int channelIndex, int inputIndex, int durationMs, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				DurationMs = durationMs;
			}
		}

		/// <summary>
		/// Internal message class representing a Source-Specific Multicast configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class EnableSourceSpecificMulticastMessage : ConfigBaseMessage
		{
			public bool IsEnabled { get; }

			internal EnableSourceSpecificMulticastMessage(int channelIndex, int inputIndex, bool isEnabled, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				IsEnabled = isEnabled;
			}
		}


		/// <summary>
		/// Internal message class representing a Source-Specific Multicast mode configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class SetSourceSpecificMulticastModeMessage : ConfigBaseMessage
		{
			public string Mode { get; }

			internal SetSourceSpecificMulticastModeMessage(int channelIndex, int inputIndex, string mode, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				Mode = mode;
			}
		}

		/// <summary>
		/// Internal message class representing SMPTE 2022-7 configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class EnableSmpte20227Message : ConfigBaseMessage
		{
			public bool IsEnabled { get; }

			internal EnableSmpte20227Message(int channelIndex, int inputIndex, bool isEnabled, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				IsEnabled = isEnabled;
			}
		}

		/// <summary>
		/// Internal message class representing SMPTE IP address configuration.
		/// </summary>
		public sealed class SetSmpte20227IpAddressMessage : ConfigBaseMessage
		{
			public string IpAddress { get; }

			internal SetSmpte20227IpAddressMessage(int channelIndex, int inputIndex, string ipAddress, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				IpAddress = ipAddress;
			}
		}


		/// <summary>
		/// Internal message class representing SMPTE port configuration.
		/// </summary>
		public sealed class SetSmpte20227PortMessage : ConfigBaseMessage
		{
			public int Port { get; }

			internal SetSmpte20227PortMessage(int channelIndex, int inputIndex, int port, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				Port = port;
			}
		}

		/// <summary>
		/// Internal message class representing the SMPTE 2022-7 Ethernet input interface configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class SetSmpte20227InterfaceMessage : ConfigBaseMessage
		{
			public string InterfaceName { get; }

			internal SetSmpte20227InterfaceMessage(int channelIndex, int inputIndex, string interfaceName, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				InterfaceName = interfaceName;
			}
		}


		/// <summary>
		/// Internal message class representing SMPTE 2022-7 network skew configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class SetSmpte20227SkewMessage : ConfigBaseMessage
		{
			public string Skew { get; }

			internal SetSmpte20227SkewMessage(int channelIndex, int inputIndex, string skew, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				Skew = skew;
			}
		}


		/// <summary>
		/// Internal message class representing a custom SMPTE 2022-7 network skew configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class SetSmpte20227CustomSkewMessage : ConfigBaseMessage
		{
			public int CustomSkewMs { get; }

			internal SetSmpte20227CustomSkewMessage(int channelIndex, int inputIndex, int customSkewMs, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				CustomSkewMs = customSkewMs;
			}
		}


		/// <summary>
		/// Internal message class representing TS VBR stream support configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class EnableInputVbrMessage : ConfigBaseMessage
		{
			public bool IsEnabled { get; }

			internal EnableInputVbrMessage(int channelIndex, int inputIndex, bool isEnabled, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				IsEnabled = isEnabled;
			}
		}


		/// <summary>
		/// Internal message class representing the low-latency FEC configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class EnableLowLatencyFecMessage : ConfigBaseMessage
		{
			public bool IsEnabled { get; }

			internal EnableLowLatencyFecMessage(int channelIndex, int inputIndex, bool isEnabled, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				IsEnabled = isEnabled;
			}
		}


		/// <summary>
		/// Internal message class representing the acceleration type configuration.
		/// Hidden from external consumers.
		/// </summary>
		public sealed class SetAccelerationTypeMessage : ConfigBaseMessage
		{
			public string Type { get; }

			internal SetAccelerationTypeMessage(int channelIndex, int inputIndex, string type, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				Type = type;
			}
		}

	}
}
