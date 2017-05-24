using System;
using UnityEngine;

/** 
 * Author:      Tobias Bollinger
 * Create Date: 15.02.2017
 */
namespace Assets.Script.Helpers
{
    /// <summary>
    /// Implementation of the InterpolationHelper with a linear interpolation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LerpHelper<T> : InterpolationHelper<T>
    {
        public LerpHelper(T start, T end, float travelTime) : base(start, end, travelTime)
        {
        }

        public LerpHelper(float speed, T start, T end) : base(speed, start, end)
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
                t = distCovered / interpolationLength;
            }
            else
            {
                t = Time.time / (_startTime + _travelTime);
            }

            if (t >= 1) goalReached = true;

            switch (_InterpolationType)
            {
                case InterpolationType.Vector:
                    return (T) Convert.ChangeType(Vector3.Lerp((Vector3) _start, (Vector3) _end, t), typeof(T));
                case InterpolationType.Float:
                    return (T)Convert.ChangeType(Mathf.Lerp((float)_start, (float)_end, t), typeof(T));
                case InterpolationType.Quaternion:
                    return (T)Convert.ChangeType(Quaternion.Lerp((Quaternion)_start, (Quaternion)_end, t), typeof(T));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
