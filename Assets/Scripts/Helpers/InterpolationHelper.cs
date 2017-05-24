using UnityEngine;

/** 
 * Author:      Tobias Bollinger
 * Create Date: 15.02.2017
 */
namespace Assets.Script.Helpers
{
    /// <summary>
    /// Abstract class for building an interpolation helper like an linear interpolation.
    /// </summary>
    /// <typeparam name="T">The type use for interpolation. Can be a Vector, Quaternion or a float</typeparam>
    public abstract class InterpolationHelper<T>
    {
        protected readonly object _start;
        protected readonly object _end;
        protected readonly float _travelTime;
        protected readonly float interpolationLength;
        protected readonly float _startTime;
        protected readonly float _speed;
        protected readonly bool _useSpeed = false;
        protected InterpolationType _InterpolationType;

        protected enum InterpolationType
        {
            Vector, Float, Quaternion
        }

        /// <summary>
        /// Initialize the helper with a start value, a end value and a travelTime
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="travelTime"></param>
        protected InterpolationHelper(T start, T end, float travelTime)
        {
            _start = start;
            _end = end;
            
            _travelTime = travelTime;
            _startTime = Time.time;
            
            if (_start is Vector3)
            {
                _InterpolationType = InterpolationType.Vector;
                interpolationLength = Vector3.Distance((Vector3)_start, (Vector3)_end);
            }
            else if (typeof(T) == typeof(float))
            {
                _InterpolationType = InterpolationType.Float;
                interpolationLength = (float) _end - (float) _start;
            }
            else if (typeof(T) == typeof(Quaternion))
            {
                _InterpolationType = InterpolationType.Quaternion;
                interpolationLength = Quaternion.Angle((Quaternion)_start, (Quaternion)_end);
            }
        }

        /// <summary>
        /// Initialize the helper with a speed, a start value and a end value.
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        protected InterpolationHelper(float speed, T start, T end) : this(start, end, speed)
        {
            _useSpeed = true;
            _speed = speed;
        }

        /// <summary>
        /// Returns the current value calculated with Time.time.
        /// </summary>
        /// <param name="goalReached">true when the goal(end value) is reached</param>
        /// <returns></returns>
        public abstract T CurrentValue(out bool goalReached);
    }
}
