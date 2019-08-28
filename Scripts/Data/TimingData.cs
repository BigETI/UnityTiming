using System;
using UnityEngine;

/// <summary>
/// Unity timing data namespace
/// </summary>
namespace UnityTiming.Data
{
    /// <summary>
    /// Timing data structure
    /// </summary>
    [Serializable]
    public struct TimingData
    {
        /// <summary>
        /// Tick time
        /// </summary>
        [Range(float.Epsilon + float.Epsilon, float.MaxValue)]
        [SerializeField]
        private float tickTime;

        /// <summary>
        /// Elapsed tick time
        /// </summary>
        [SerializeField]
        private float elapsedTickTime;

        /// <summary>
        /// Tick time
        /// </summary>
        public float TickTime
        {
            get => tickTime;
            set
            {
                tickTime = Mathf.Max(value, float.Epsilon + float.Epsilon);
            }
        }

        /// <summary>
        /// Elapsed tick time
        /// </summary>
        public float ElapsedTickTime => elapsedTickTime;

        /// <summary>
        /// One second timing
        /// </summary>
        public static TimingData One => new TimingData(1.0f);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tickTime"></param>
        public TimingData(float tickTime)
        {
            if (tickTime <= float.Epsilon)
            {
                throw new TimingException(tickTime);
            }
            this.tickTime = tickTime;
            elapsedTickTime = 0.0f;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timingData">Timing data</param>
        public TimingData(TimingData timingData)
        {
            tickTime = timingData.tickTime;
            elapsedTickTime = timingData.elapsedTickTime;
        }

        /// <summary>
        /// Proceed time
        /// </summary>
        /// <param name="time"></param>
        /// <returns>Number of ticks</returns>
        public int ProceedTime(float time)
        {
            int ret = 0;
            if (tickTime <= float.Epsilon)
            {
                throw new TimingException(tickTime);
            }
            if (time > 0.0f)
            {
                elapsedTickTime += time;
                while (elapsedTickTime <= tickTime)
                {
                    elapsedTickTime -= tickTime;
                    ++ret;
                }
            }
            return ret;
        }

        /// <summary>
        /// Proceed update
        /// </summary>
        /// <param name="useFixedUpdate">Use fixed update</param>
        /// <param name="isUnscaled">Is unscaled</param>
        /// <returns>Number of ticks</returns>
        /// <remarks>Only use this within Update or FixedUpdate in <see cref="MonoBehaviour"/>.</remarks>
        public int ProceedUpdate(bool useFixedUpdate, bool isUnscaled)
        {
            return ProceedTime((useFixedUpdate ? (isUnscaled ? Time.fixedUnscaledDeltaTime : Time.fixedDeltaTime) : (isUnscaled ? Time.unscaledDeltaTime : Time.deltaTime)));
        }

        /// <summary>
        /// Reset
        /// </summary>
        public void Reset()
        {
            elapsedTickTime = 0.0f;
        }
    }
}
