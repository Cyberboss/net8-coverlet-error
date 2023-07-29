using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Library
{
	/// <summary>
	/// File logging configuration options.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Default value for <see cref="LogLevel"/>.
		/// </summary>
		const LogLevel DefaultLogLevel = LogLevel.Debug;

		/// <summary>
		/// The minimum <see cref="Microsoft.Extensions.Logging.LogLevel"/> to display in logs.
		/// </summary>
		[JsonProperty]
		LogLevel LogLevel { get; set; }

		/// <summary>
		/// Program entrypoint.
		/// </summary>
		public static void Main()
		{
		}
	}
}
