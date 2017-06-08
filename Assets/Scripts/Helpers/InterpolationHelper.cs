using Assets.Scripts.Extensions;
using UnityEngine;

/** 
 * Author:      Tobias Bollinger
 * Create Date: 15.02.2017
 */
namespace Assets.Scripts.Helpers
{
    /// <summary>
    /// Abstract class for building an interpolation helper like a linear interpolation.
    /// </summary>
    /// <typeparam name="T">The type use for interpolation. Can be a Vector, Quaternion or a float</typeparam>
    public abstract class InterpolationHelper<T>
    {
        protected readonly object Start;
        protected readonly object End;
        protected readonly float TravelTime;
        protected readonly float InterpolationLength;
        protected readonly float StartTime;
        protected readonly float Speed;
        protected readonly bool UseSpeed = false;
        protected InterpolationType _InterpolationType;

        protected enum InterpolationType
        {
            Vector, Float, Quaternion, Color
        }

        /// <summary>
        /// Initialize the helper with a start value, a end value and a travelTime
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="travelTimeOrSpeed"></param>
        /// <param name="useSpeed">
        /// True  : The value will be interpolated in the specified speed
        /// False : The value will reach end in the specified time.
        /// </param>
        protected InterpolationHelper(T start, T end, float travelTimeOrSpeed, bool useSpeed)
        {
            Start = start;
            End = end;

            TravelTime = travelTimeOrSpeed;
            StartTime = Time.time;

            if (Start is Vector3)
            {
                _InterpolationType = InterpolationType.Vector;
                InterpolationLength = Vector3.Distance((Vector3)Start, (Vector3)End);
            }
            else if (typeof(T) == typeof(float))
            {
                _InterpolationType = InterpolationType.Float;
				InterpolationLength = Mathf.Abs((float)End - (float)Start);
            }
            else if (typeof(T) == typeof(Quaternion))
            {
                _InterpolationType = InterpolationType.Quaternion;
                InterpolationLength = Quaternion.Angle((Quaternion)Start, (Quaternion)End);
            }
            else if (typeof(T) == typeof(Color))
            {
                _InterpolationType = InterpolationType.Color;
                InterpolationLength = ((Color) Start).Distance((Color) End);
            }

            UseSpeed = useSpeed;
            Speed = travelTimeOrSpeed;
        }

        /// <summary>
        /// Returns the current value calculated with Time.time.
        /// </summary>
        /// <param name="goalReached">true when the goal(end value) is reached</param>
        /// <returns></returns>
        public abstract T CurrentValue(out bool goalReached);
    }
}
