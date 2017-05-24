using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * Author:      Tobias Bollinger
 * Create Date: 15.02.2017
 */
namespace Assets.Script.Helpers
{
    /// <summary>
    /// Helper to build a queue of corutines. 
    /// Allow to add corutines to the queue and the will be process synchronous.
    /// </summary>
    public class CorutineQueue : MonoBehaviour
    {
        private readonly Queue<IEnumerator> _queue;

        private UnityEngine.Coroutine _currentRoutine;
        private bool _isRunning;

        /// <summary>
        /// Initialize a new CorutineQueue helper class.
        /// </summary>
        public CorutineQueue()
        {
            _isRunning = false;
        }

        /// <summary>
        /// Initialize a new CorutineQueue helper class with a given amount of routines already added to the queue. 
        /// </summary>
        /// <param name="routines"></param>
        public CorutineQueue(IEnumerable<IEnumerator> routines)
        {
            _queue = new Queue<IEnumerator>(routines);
            _isRunning = true;
            StartCoroutine(Process());
        }

        private IEnumerator Process()
        {
            while (_isRunning && _queue.Count > 0)
            {
                yield return StartCoroutine(_queue.Dequeue());
            }
        }

        /// <summary>
        /// Add a new corutine to the queue and start process the queue.
        /// </summary>
        /// <param name="routine"></param>
        public void EnqueAndPlay(IEnumerator routine)
        {
            Enqueue(routine);
            if (!_isRunning) StartCoroutine(Process());
        }

        /// <summary>
        /// Add a new corutine to the queue.
        /// </summary>
        /// <param name="routine"></param>
        public void Enqueue(IEnumerator routine)
        {
            _queue.Enqueue(routine);
        }

        /// <summary>
        /// Start processing the queue.
        /// </summary>
        public void Start()
        {
            _isRunning = true;
            StartCoroutine(Process());
        }

        /// <summary>
        /// Stop processing the queue.
        /// <remarks>Already running corutine will finish processing.</remarks>
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
        }

    }
}
