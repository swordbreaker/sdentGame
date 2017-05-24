using System.Collections;

/** 
 * Author:      Tobias Bollinger
 * Create Date: 15.02.2017
 */
namespace Assets.Script.Helpers
{
    /// <summary>
    /// Helper to only allow one corutine to process and cancel all other if one is already running.
    /// </summary>
    public class CorutineBlocker
    {
        private bool _isRunning;

        public CorutineBlocker()
        {
            _isRunning = false;
        }

        private IEnumerator Process(IEnumerator routine)
        {
            if (!_isRunning)
            {
                _isRunning = true;
                yield return routine;
                _isRunning = false;
            }
        }

        /// <summary>
        /// Try to run a new corutine cancel it when one is already running.
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        public IEnumerator TryRun(IEnumerator routine)
        {
            return Process(routine);
        }
    }
}
