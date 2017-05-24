using System;
using UnityEngine;

/** 
 * Author:      Tobias Bollinger
 * Create Date: 15.02.2017
 */
namespace Assets.Script.Helpers
{
    /// <summary>
    /// Helper for calculate a ballistic behavior. 
    /// </summary>
    public class BallisticHelper
    {
        private readonly float _angle;
        private readonly Vector3 _v0;
        private readonly Vector3 _startPos;

        /// <summary>
        /// Initialize a new Ballistic helper class stores the angle, the start velocity v0 and the start position 
        /// to easily calculate a point with only the time.
        /// </summary>
        /// <param name="angle">Angle in radian</param>
        /// <param name="v0">start velocity</param>
        /// <param name="startPos">start position</param>
        public BallisticHelper(float angle, Vector3 v0, Vector3 startPos)
        {
            _angle = angle;
            _v0 = v0;
            _startPos = startPos;
        }

        /// <summary>
        /// Calculates a new point.
        /// </summary>
        /// <param name="t">t: 0 returns the start position. t: 1 returns the end position</param>
        /// <returns></returns>
        public Vector3 CalculatePoint(float t)
        {
            return _startPos + CalculatePoint(t, _angle, _v0);
        }

        /// <summary>
        /// Calculate the angle with a given start position, end position and a velocity.
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="velocity"></param>
        /// <returns>The angle in radian</returns>
        public static float CalculateAngle(Vector3 startPos, Vector3 endPos, float velocity)
        {
            var relativeEndPost = endPos - startPos;
            var x = relativeEndPost.x;
            var y = relativeEndPost.y;
            var v2 = velocity*velocity;
            var v4 = v2*v2;
            var g = -Physics.gravity.y;

            var sqr = v4 - g*(g*x*x + 2*y*v2);

            if(sqr < 0) throw new ArgumentException("No solution");

            var angle = Mathf.Atan(v2 + Mathf.Sqrt(sqr) / g*x);

            return angle;
        }

        /// <summary>
        /// Calculates the velocity vector form vector zero to a given end position and an given angle. 
        /// The velocity is calculated that the time t is 1 when the end position is reached
        /// </summary>
        /// <param name="endPos">end position</param>
        /// <param name="angle">angle in radian</param>
        /// <returns>a Vector3 velocity</returns>
        public static Vector3 CalculateVelocity(Vector3 endPos, float angle)
        {
            var g = -Physics.gravity.y;
            var vx = endPos.x/(Mathf.Cos(angle));
            var vz = endPos.z/(Mathf.Cos(angle));
            var vy = (endPos.y + g/2)/Mathf.Sin(angle);

            return new Vector3(vx, vy, vz);
        }

        /// <summary>
        /// Calculates a point in on the ballistic arc to an given time.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="angle">Angle in radian</param>
        /// <param name="velocity">Velocity as an Vector3</param>
        /// <returns></returns>
        public static Vector3 CalculatePoint(float time, float angle, Vector3 velocity)
        {
            var g = -Physics.gravity.y;
            var x = velocity.x*time*Mathf.Cos(angle);
            var y = velocity.y*time*Mathf.Sin(angle) - (g/2)*time*time;
            var z = velocity.z * time * Mathf.Cos(angle);

            return new Vector3(x, y, z);
        }
    }
}
