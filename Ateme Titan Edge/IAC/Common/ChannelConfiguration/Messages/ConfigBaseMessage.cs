using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;
using Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Common.Enums;

namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Common.ChannelConfiguration.Messages
{
	/// <summary>
	/// Represents the base class for all protocol messages.  
	/// Provides common properties for protocol trigger identification, channel association, and input targeting.
	/// </summary>
	public abstract class ConfigBaseMessage : Message
	{
		/// <summary>
		/// Gets the protocol trigger parameter ID for this message.
		/// </summary>
		public int ProtocolTriggerId { get; }

		/// <summary>
		/// Gets the channel index for this message.
		/// </summary>
		public int ChannelIndex { get; }

		/// <summary>
		/// Gets the input index for this message.
		/// </summary>
		public int InputIndex { get; }

		/// <summary>
		/// Gets the config type (Encoder/Decoder) for this message.
		/// </summary>
		public ConfigType ConfigType { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigBaseMessage"/> class.
		/// </summary>
		/// <param name="channelIndex">The index of the channel.</param>
		/// <param name="inputIndex">The index of the input.</param>
		/// <param name="configType">The configuration type, used to determine the protocol trigger ID.</param>
		protected ConfigBaseMessage(int channelIndex, int inputIndex, ConfigType configType)
		{
			ChannelIndex = channelIndex;
			InputIndex = inputIndex;
			ConfigType = configType;
		}
	}


}
