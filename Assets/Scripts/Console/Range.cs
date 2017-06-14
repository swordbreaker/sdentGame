using System;

namespace Assets.Scripts.Console
{
    /// <summary>
    /// Imutable struct to store a Range
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public struct 
        Range<T>
    {
        public delegate bool InRangeCheckerFunction(T value, Range<T> range);

        public readonly T Form;
        public readonly T Till;
        private InRangeCheckerFunction _inRangeChecker;

        /// <summary>
        /// Create a new Range
        /// </summary>
        /// <param name="form">Inclusive</param>
        /// <param name="till">Inclusive</param>
        /// <param name="inRangeChecker">A function which takes a value and a range and outputs if it is in range</param>
        public Range(T form, T till, InRangeCheckerFunction inRangeChecker)
        {
            Form = form;
            Till = till;
            _inRangeChecker = inRangeChecker;
        }

        public bool IsInRange(T value)
        {
            return _inRangeChecker(value, this);
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Form, Till);
        }
    }
}
