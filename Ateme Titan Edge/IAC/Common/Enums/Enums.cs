
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Common.Enums
{
	/// <summary>
	/// Defines the configuration mode of the device.
	/// </summary>
	public enum ConfigType
	{
		/// <summary>
		/// Encoder mode – used for converting input signals to compressed or network streams.
		/// </summary>
		Encoder = 0,

		/// <summary>
		/// Decoder mode – used for converting incoming streams to output signals.
		/// </summary>
		Decoder = 1,
	}

	/// <summary>
	/// Specifies the mode for Source-Specific Multicast (SSM) filtering.
	/// </summary>
	public enum SourceSpecificMulticastMode
	{
		/// <summary>
		/// Include mode.
		/// </summary>
		Include,

		/// <summary>
		/// Exclude mode.
		/// </summary>
		Exclude
	}

	/// <summary>
	/// Determines the decoder or gateway input type.
	/// </summary>
	/// <summary>
	/// Determines the decoder or gateway input type.
	/// </summary>
	public enum InputType
	{
		/// <summary>
		/// IP input. Default value is "ip".
		/// </summary>
		Ip,

		/// <summary>
		/// ASI input.
		/// </summary>
		Asi,

		/// <summary>
		/// Satellite input.
		/// </summary>
		Sat,

		/// <summary>
		/// Terrestrial input.
		/// </summary>
		Terrestrial,

		/// <summary>
		/// Zixi input.
		/// </summary>
		Zixi,

		/// <summary>
		/// SRT input.
		/// </summary>
		Srt,

		/// <summary>
		/// RIST input.
		/// </summary>
		Rist,

		/// <summary>
		/// Internal input.
		/// </summary>
		Internal,

		/// <summary>
		/// HLS input.
		/// </summary>
		Hls,

		/// <summary>
		/// RTMP input.
		/// </summary>
		Rtmp
	}

	/// <summary>
	/// Defines the network skew as per SMPTE 2022-7.
	/// Low skew is below 10 ms, moderate is below 50 ms, high is below 450 ms,
	/// and custom is user-defined in customSkew.
	/// </summary>
	public enum SmpteSkew
	{
		/// <summary>Low skew (below 10 ms).</summary>
		Low,

		/// <summary>Moderate skew (below 50 ms).</summary>
		Moderate,

		/// <summary>High skew (below 450 ms).</summary>
		High,

		/// <summary>Custom skew defined by the user.</summary>
		Custom
	}

	/// <summary>
	/// Defines the additional acceleration hardware or API usage.
	/// </summary>
	public enum AccelerationType
	{
		/// <summary>Disabled acceleration.</summary>
		Disabled,

		/// <summary>Rivermax acceleration (default).</summary>
		Rivermax
	}

	/// <summary>
	/// Represents the available SDI (Serial Digital Interface) input/output formats.
	/// </summary>
	public enum SdiType
	{
		/// <summary>
		/// No SDI signal is used.
		/// </summary>
		Disabled,

		/// <summary>
		/// Automatically detect the SDI signal type.
		/// </summary>
		AutoSdi,

		/// <summary>
		/// Standard-definition SDI format.
		/// </summary>
		SdSdi,

		/// <summary>
		/// High-definition SDI format (1080i or similar).
		/// </summary>
		HdSdi,

		/// <summary>
		/// 720p progressive HD SDI format.
		/// </summary>
		_720pSdi,

		/// <summary>
		/// 1080p progressive HD SDI format.
		/// </summary>
		_1080pSdi,

		/// <summary>
		/// Ultra-high-definition SDI format (4K and above).
		/// </summary>
		UhdSdi
	}

	/// <summary>
	/// Specifies the colorimetry conversion types available for output connectors.
	/// </summary>
	public enum ColorimetryConversion
	{
		/// <summary>
		/// Default colorimetry conversion (<c>bt709</c>).
		/// </summary>
		Default,

		/// <summary>
		/// BT.601 525i conversion.
		/// </summary>
		Bt601_525i,

		/// <summary>
		/// BT.601 625i conversion.
		/// </summary>
		Bt601_625i,

		/// <summary>
		/// BT.709 conversion.
		/// </summary>
		Bt709,

		/// <summary>
		/// SDR wide color gamut conversion.
		/// </summary>
		SdrWcg,

		/// <summary>
		/// PQ10 HDR conversion.
		/// </summary>
		Pq10,

		/// <summary>
		/// HDR10 conversion.
		/// </summary>
		Hdr10,

		/// <summary>
		/// HLG HDR conversion.
		/// </summary>
		Hlg
	}

	/// <summary>
	/// Represents the possible IUT (Input/Lookup Table) names for colorimetry conversion.
	/// </summary>
	public enum IutName
	{
		/// <summary>
		/// Default scaling mode (<c>eotfScaling</c>).
		/// </summary>
		Default,

		/// <summary>
		/// EOTF scaling mode.
		/// </summary>
		EotfScaling,

		/// <summary>
		/// Hable tone-mapping mode.
		/// </summary>
		Hable,

		/// <summary>
		/// Normative tone-mapping mode.
		/// </summary>
		Normative,

		/// <summary>
		/// NBCU1 mode. This mode does not detect if the LUT is required before applying it.
		/// </summary>
		Nbcu1
	}

	/// <summary>
	/// Extension methods for <see cref="IutName"/>.
	/// </summary>
	public static class IutNameExtensions
	{
		/// <summary>
		/// Converts a <see cref="IutName"/> value to the API string format.
		/// </summary>
		/// <param name="iutName">The IUT name enum value.</param>
		/// <returns>The API string representation of the IUT name.</returns>
		public static string ToApiString(this IutName iutName)
		{
			switch (iutName)
			{
				case IutName.EotfScaling:
				case IutName.Default: // Default maps to "eotfScaling"
					return "eotfScaling";
				case IutName.Hable:
					return "hable";
				case IutName.Normative:
					return "normative";
				case IutName.Nbcu1:
					return "nbcu1";
				default:
					throw new ArgumentOutOfRangeException(nameof(iutName), iutName, null);
			}
		}
	}

	/// <summary>
	/// Extension methods for <see cref="ColorimetryConversion"/>.
	/// </summary>
	public static class ColorimetryConversionExtensions
	{
		/// <summary>
		/// Converts a <see cref="ColorimetryConversion"/> value to the API string format.
		/// </summary>
		/// <param name="conversion">The colorimetry conversion value.</param>
		/// <returns>The API string representation of the conversion.</returns>
		public static string ToApiString(this ColorimetryConversion conversion)
		{
			switch (conversion)
			{
				case ColorimetryConversion.Bt601_525i:
					return "bt601_525i";
				case ColorimetryConversion.Bt601_625i:
					return "bt601_625i";
				case ColorimetryConversion.Bt709:
					return "bt709";
				case ColorimetryConversion.SdrWcg:
					return "sdrWcg";
				case ColorimetryConversion.Pq10:
					return "pq10";
				case ColorimetryConversion.Hdr10:
					return "hdr10";
				case ColorimetryConversion.Hlg:
					return "hlg";
				case ColorimetryConversion.Default:
				default:
					return "bt709";
			}
		}
	}

	public static class SdiTypeExtensions
	{
		/// <summary>
		/// Converts the <see cref="SdiType"/> value into the exact string format required by the API.
		/// </summary>
		public static string ToApiString(this SdiType type)
		{
			switch (type)
			{
				case SdiType.Disabled: return "disabled";
				case SdiType.AutoSdi: return "auto_sdi";
				case SdiType.SdSdi: return "sd_sdi";
				case SdiType.HdSdi: return "hd_sdi";
				case SdiType._720pSdi: return "720p_sdi";
				case SdiType._1080pSdi: return "1080p_sdi";
				case SdiType.UhdSdi: return "uhd_sdi";
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
	}


	/// <summary>
	/// Helper class to get string values from <see cref="InputType"/>.
	/// </summary>
	public static class InputTypeExtensions
	{
		/// <summary>
		/// Converts an <see cref="InputType"/> enum value to its corresponding string representation
		/// used in the API or configuration.
		/// </summary>
		/// <param name="type">The <see cref="InputType"/> value to convert.</param>
		/// <returns>The string representation of the <see cref="InputType"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if the <paramref name="type"/> is not a valid <see cref="InputType"/> value.
		/// </exception>
		public static string ToApiString(this InputType type)
		{
			switch (type)
			{
				case InputType.Ip:
					return "ip";
				case InputType.Asi:
					return "asi";
				case InputType.Sat:
					return "sat";
				case InputType.Terrestrial:
					return "terrestrial";
				case InputType.Zixi:
					return "zixi";
				case InputType.Srt:
					return "srt";
				case InputType.Rist:
					return "rist";
				case InputType.Internal:
					return "internal";
				case InputType.Hls:
					return "hls";
				case InputType.Rtmp:
					return "rtmp";
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}

	}
}
