using System;
using UnityEngine;

/** 
 * Author:      Tobias Bollinger
 * Create Date: 15.02.2017
 */
namespace Assets.Script.Helpers
{
    /// <summary>
    /// Implementation of the InterpolationHelper with a spherical linear interpolation.
    /// <remarks>Cannot be used with a float</remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SlerpHelper<T> : InterpolationHelper<T>
    {
        public SlerpHelper(T start, T end, float travelTime) : base(start, end, travelTime)
        {
        }

        public SlerpHelper(float speed, T start, T end) : base(speed, start, end)
        {
        }

        public override T CurrentValue(out bool goalReached)
        {
            goalReached = false;
            if (interpolationLength < Mathf.Epsilon)
            {
                goalReached = true;
                return (T)_start;
            }
            float t;

            if (_useSpeed)
            {
                var distCovered = (Time.time - _startTime) * _speed;
                t = distCovered/interpolationLength;
            }
            else
            {
                t = Time.time / (_startTime + _travelTime);
            }
            
            if (t >= 1) goalReached = true;

            switch (_InterpolationType)
            {
                case InterpolationType.Vector:
                    return (T)Convert.ChangeType(Vector3.Slerp((Vector3)_start, (Vector3)_end, t), typeof(T));
                case InterpolationType.Float:
                    throw new NotSupportedException("Float cannot be slerped");
                case InterpolationType.Quaternion:
                    return (T)Convert.ChangeType(Quaternion.Slerp((Quaternion)_start, (Quaternion)_end, t), typeof(T));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
