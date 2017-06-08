using System;
using UnityEngine;

/** 
 * Author:      Tobias Bollinger
 * Create Date: 15.02.2017
 */
namespace Assets.Scripts.Helpers
{
    /// <summary>
    /// Implementation of the InterpolationHelper with a linear interpolation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LerpHelper<T> : InterpolationHelper<T>
    {
        public LerpHelper(T start, T end, float travelTimeOrSpeed, bool useSpeed) : base(start, end, travelTimeOrSpeed, useSpeed) { }

        public override T CurrentValue(out bool goalReached)
        {
            goalReached = false;
            if (InterpolationLength < Mathf.Epsilon)
            {
                goalReached = true;
                return (T)Start;
            }

            float t;
            if (UseSpeed)
            {
                var distCovered = (Time.time - StartTime) * Speed;
                t = distCovered / InterpolationLength;
            }
            else
            {
                t = (Time.time - StartTime) / TravelTime;
            }

            if (t >= 1) goalReached = true;

            switch (_InterpolationType)
            {
                case InterpolationType.Vector:
                    return (T) Convert.ChangeType(Vector3.Lerp((Vector3) Start, (Vector3) End, t), typeof(T));
                case InterpolationType.Float:
                    return (T)Convert.ChangeType(Mathf.Lerp((float)Start, (float)End, t), typeof(T));
                case InterpolationType.Quaternion:
                    return (T)Convert.ChangeType(Quaternion.Lerp((Quaternion)Start, (Quaternion)End, t), typeof(T));
                case InterpolationType.Color:
                    return (T) Convert.ChangeType(Color.Lerp((Color) Start, (Color) End, t), typeof(T));

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
