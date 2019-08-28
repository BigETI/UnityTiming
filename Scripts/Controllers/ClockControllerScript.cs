using UnityEngine;
using UnityEngine.Events;
using UnityTiming.Data;

/// <summary>
/// Unity timing controllers namespace
/// </summary>
namespace UnityTiming.Controllers
{
    /// <summary>
    /// Clock manager script class
    /// </summary>
    public class ClockControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Timing
        /// </summary>
        [SerializeField]
        private TimingData timing = TimingData.One;

        /// <summary>
        /// Is running
        /// </summary>
        [SerializeField]
        private bool isRunning = true;

        /// <summary>
        /// Use fixed update
        /// </summary>
        [SerializeField]
        private bool useFixedUpdate = default;

        /// <summary>
        /// Unscaled time
        /// </summary>
        [SerializeField]
        private bool unscaledTime = default;

        /// <summary>
        /// On tick
        /// </summary>
        [SerializeField]
        private UnityEvent onTick = default;

        /// <summary>
        /// Tick time
        /// </summary>
        public float TickTime
        {
            get => timing.TickTime;
            set => timing.TickTime = value;
        }

        /// <summary>
        /// Is running
        /// </summary>
        public bool IsRunning
        {
            get => isRunning;
            set
            {
                if (isRunning != value)
                {
                    timing.Reset();
                    isRunning = value;
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (isRunning && (!useFixedUpdate))
            {
                int ticks = timing.ProceedUpdate(false, unscaledTime);
                for (int i = 0; i < ticks; i++)
                {
                    onTick.Invoke();
                }
            }
        }

        /// <summary>
        /// Fixed update
        /// </summary>
        private void FixedUpdate()
        {
            if (isRunning && useFixedUpdate)
            {
                int ticks = timing.ProceedUpdate(true, unscaledTime);
                for (int i = 0; i < ticks; i++)
                {
                    onTick.Invoke();
                }
            }
        }
    }
}
