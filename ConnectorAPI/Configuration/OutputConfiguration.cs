namespace Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

	/// <summary>
	/// Builder class for constructing and sending output configuration messages for a specific channel and connector.
	/// <para>
	/// This class provides a fluent API to configure various output parameters such as SDI type, display name, colorimetry settings, colorimetry conversion, and IUT (Lookup Table) name.
	/// </para>
	/// <example>
	/// <code>
	/// var config = new OutputConfiguration(0, 1, ConfigType.Decoder)
	///     .ChangeType(SdiType.HdSdi)
	///     .SetName("Main Output")
	///     .EnableColorimetry(true)
	///     .SetColorimetryConversion(ColorimetryConversion.Hdr10)
	///     .SetIutName(IutName.Hable);
	/// config.Send(client);
	/// </code>
	/// </example>
	/// <remarks>
	/// Use the <see cref="Send(IAtemeTitanEdgeClient)"/> method to send all configured messages at once.
	/// </remarks>
	/// </summary>
	public sealed class OutputConfiguration : IAtemeTitanEdgeConfig
	{
		private readonly int _connectorIndex;
		private readonly int _channelIndex;
		private readonly ConfigType _type;
		private readonly List<Message> _messages = new List<Message>();

		/// <summary>
		/// Initializes a new instance of the <see cref="OutputConfiguration"/> class for the specified input and channel.
		/// </summary>
		/// <param name="connectorIndex">The input index of the channel.</param>
		/// <param name="channelIndex">The channel index.</param>
		/// <param name="type">The type of the channel.</param>
		public OutputConfiguration(int connectorIndex, int channelIndex, ConfigType type)
		{
			_connectorIndex = connectorIndex;
			_channelIndex = channelIndex;
			_type = type;
		}

		/// <summary>
		/// Changes the SDI type for the current output configuration and queues the update message.
		/// </summary>
		/// <param name="newType">
		/// The new <see cref="SdiType"/> to set.  
		/// Defaults to <see cref="SdiType.AutoSdi"/> if not specified.
		/// </param>
		/// <returns>
		/// The current <see cref="OutputConfiguration"/> instance for method chaining.
		/// </returns>
		public OutputConfiguration ChangeType(SdiType newType = SdiType.AutoSdi)
		{
			_messages.Add(new OutputConnectorTypeMessage(_channelIndex, _connectorIndex, newType.ToApiString(), _type));
			return this;
		}

		/// <summary>
		/// Sets the display name of the current output connector.
		/// </summary>
		/// <param name="name">The name to assign to the output connector. Defaults to an empty string.</param>
		/// <returns>The updated <see cref="OutputConfiguration"/> instance for method chaining.</returns>
		public OutputConfiguration SetName(string name = "")
		{
			_messages.Add(new OutputConnectorNameMessage(_channelIndex, _connectorIndex, name, _type));
			return this;
		}

		/// <summary>
		/// Enables or disables colorimetry for the current output connector.
		/// </summary>
		/// <param name="isEnabled">
		/// If set to <c>true</c>, colorimetry will be enabled; otherwise, it will be disabled. Defaults to <c>false</c>.
		/// </param>
		/// <returns>The updated <see cref="OutputConfiguration"/> instance for method chaining.</returns>
		public OutputConfiguration EnableColorimetry(bool isEnabled = false)
		{
			_messages.Add(new OutputConnectorColorimetryMessage(_channelIndex, _connectorIndex, isEnabled, _type));
			return this;
		}

		/// <summary>
		/// Sets the colorimetry conversion for the current output connector.
		/// </summary>
		/// <param name="conversion">
		/// The desired <see cref="ColorimetryConversion"/> type. Defaults to <see cref="ColorimetryConversion.Default"/>.
		/// </param>
		/// <returns>The updated <see cref="OutputConfiguration"/> instance for method chaining.</returns>
		public OutputConfiguration SetColorimetryConversion(ColorimetryConversion conversion = ColorimetryConversion.Default)
		{
			_messages.Add(new OutputConnectorColorimetryConversionMessage(_channelIndex, _connectorIndex, conversion.ToApiString(), _type));
			return this;
		}

		/// <summary>
		/// Sets the IUT (Lookup Table) name for the current output connector.
		/// </summary>
		/// <param name="iutName">
		/// The desired <see cref="IutName"/> type. Defaults to <see cref="IutName.Default"/> (<c>eotfScaling</c>).
		/// </param>
		/// <returns>The updated <see cref="OutputConfiguration"/> instance for method chaining.</returns>
		public OutputConfiguration SetIutName(IutName iutName = IutName.Default)
		{
			_messages.Add(new OutputConnectorIutMessage(_channelIndex, _connectorIndex, iutName.ToApiString(), _type));
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
		/// Represents a configuration message to update the SDI type of a specific output connector.
		/// </summary>
		public sealed class OutputConnectorTypeMessage : ConfigBaseMessage
		{
			/// <summary>
			/// Gets a value indicating type of an output.
			/// </summary>
			public string SdiType { get; }

			internal OutputConnectorTypeMessage(int channelIndex, int inputIndex, string sdiType, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				SdiType = sdiType;
			}
		}

		/// <summary>
		/// Represents a message to set the display name of an output connector in the device configuration.
		/// </summary>
		public sealed class OutputConnectorNameMessage : ConfigBaseMessage
		{
			/// <summary>
			/// Gets a value indicating the name of the output.
			/// </summary>
			public string Name { get; }

			internal OutputConnectorNameMessage(int channelIndex, int inputIndex, string name, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				Name = name;
			}
		}

		/// <summary>
		/// Represents a message to enable or disable colorimetry settings for an output connector.
		/// </summary>
		public class OutputConnectorColorimetryMessage : ConfigBaseMessage
		{
			/// <summary>
			/// Gets a value indicating whether colorimetry is enabled.
			/// </summary>
			public bool IsEnabled { get; }

			internal OutputConnectorColorimetryMessage(int channelIndex, int inputIndex, bool isEnabled, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				IsEnabled = isEnabled;
			}
		}

		/// <summary>
		/// Represents a message to set the colorimetry conversion for an output connector.
		/// </summary>
		public class OutputConnectorColorimetryConversionMessage : ConfigBaseMessage
		{
			/// <summary>
			/// Gets the colorimetry conversion string value.
			/// </summary>
			public string Conversion { get; }

			/// <summary>
			/// Initializes a new instance of the <see cref="OutputConnectorColorimetryConversionMessage"/> class.
			/// </summary>
			/// <param name="channelIndex">The index of the output channel.</param>
			/// <param name="inputIndex">The index of the connector input.</param>
			/// <param name="conversion">The colorimetry conversion string value.</param>
			/// <param name="configType">The type of configuration being applied.</param>
			internal OutputConnectorColorimetryConversionMessage(int channelIndex, int inputIndex, string conversion, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				Conversion = conversion;
			}
		}

		/// <summary>
		/// Represents a message to set the IUT (Lookup Table) name for colorimetry conversion.
		/// </summary>
		public class OutputConnectorIutMessage : ConfigBaseMessage
		{
			/// <summary>
			/// Gets the IUT name string used for the API call.
			/// </summary>
			public string Iut { get; }

			/// <summary>
			/// Initializes a new instance of the <see cref="OutputConnectorIutMessage"/> class.
			/// </summary>
			/// <param name="channelIndex">The index of the output channel.</param>
			/// <param name="inputIndex">The index of the connector input.</param>
			/// <param name="iut">The IUT name string formatted for the API.</param>
			/// <param name="configType">The type of configuration being applied.</param>
			internal OutputConnectorIutMessage(int channelIndex, int inputIndex, string iut, ConfigType configType)
				: base(channelIndex, inputIndex, configType)
			{
				Iut = iut;
			}
		}
	}
}
