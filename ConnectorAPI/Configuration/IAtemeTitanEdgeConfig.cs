namespace Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge
{
	using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

	/// <summary>
	/// Represents the configuration interface for Ateme Titan Edge.
	/// </summary>
	public interface IAtemeTitanEdgeConfig
	{
		/// <summary>
		/// Builds the configuration messages for Ateme Titan Edge.
		/// </summary>
		/// <returns>
		/// An array of <see cref="Message"/> objects representing the configuration.
		/// </returns>
		Message[] ToInterAppMessages();
	}
}
