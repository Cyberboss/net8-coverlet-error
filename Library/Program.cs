using Microsoft.Extensions.Logging;

namespace Library
{
	public class Program
	{
#if SHOW_REPRO
		const LogLevel DefaultLogLevel = LogLevel.Debug;
		LogLevel LogLevel { get; set; } = DefaultLogLevel;
#else
		LogLevel LogLevel { get; set; } = LogLevel.Debug;
#endif

		/// <summary>
		/// Program entrypoint.
		/// </summary>
		public static void Main()
		{
		}
	}
}
