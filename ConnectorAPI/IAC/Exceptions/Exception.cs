using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.DataMiner.Utils.AtemeTitanEdge.IAC.Exceptions
{
	public class InputValidationException : Exception
	{
		public InputValidationException(string message) : base(message) { }

		public InputValidationException(string message, Exception innerException) : base(message, innerException) { }
	}
	public class MissingTypeException : InputValidationException
	{
		public MissingTypeException() : base("Type is required.") { }
	}

	public class MissingIpConfigException : InputValidationException
	{
		public MissingIpConfigException() : base("IP configuration must be provided.") { }
	}

	public class InvalidIpAddressException : InputValidationException
	{
		public InvalidIpAddressException() : base("IP address is required.") { }
	}

	public class InvalidPortException : InputValidationException
	{
		public InvalidPortException() : base("Port must be a positive number.") { }
	}
	public class InvalidChannelIdException : InputValidationException
	{
		public InvalidChannelIdException() : base("Channel ID must be a non-negative number.") { }
	}

}
