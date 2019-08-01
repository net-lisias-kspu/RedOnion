using System;
namespace RedOnion.OS
{
	public interface IExecutable
	{
		/// <summary>
		/// Execute for the specified timeLimitMillis.
		/// </summary>
		/// <returns>Whether this executable voluntarily yielded.</returns>
		/// <param name="timeLimitMillis">Time limit millis.</param>
		bool Execute(double timeLimitMillis);
		/// <returns><c>true</c>, if executable is sleeping, <c>false</c> otherwise.</returns>
		bool IsSleeping();
	}
}
