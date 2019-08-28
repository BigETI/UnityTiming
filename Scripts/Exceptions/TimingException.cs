using System;

/// <summary>
/// Unity timing namespace
/// </summary>
namespace UnityTiming
{
    /// <summary>
    /// TIming exception class
    /// </summary>
    public class TimingException : Exception
    {
        /// <summary>
        /// Tick time
        /// </summary>
        public float TickTime { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tickTime">Tick time</param>
        public TimingException(float tickTime) : base("Tick time can't be close to zero, zero or negative.")
        {
            TickTime = tickTime;
        }
    }
}
