using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace UnityUtility
{
    /// <summary>
    /// Dispatch events to be executed on the Unity main thread.
    /// </summary>
    public class Dispatcher : Singleton<Dispatcher>
    {
        private static readonly ConcurrentBag<Action> pending = new ConcurrentBag<Action>();

        public void Invoke(Action fn)
        {
            pending.Add(fn);
        }

        private void Update()
        {
            InvokePending();
        }

        private void InvokePending()
        {
            /*
             * TODO fix :
             *
             * InvalidOperationException: Collection was modified; enumeration operation may not execute.
             * Happens when you call Dispatcher.Invoke inside of another Dispatcher.Invoke
             */

            while (!pending.IsEmpty)
            {
                pending.TryTake(out var action);

                try
                {
                    action();
                }
                catch (Exception e)
                {
                    // If there is no Catch, the pending list never clears.
                    Debug.LogError("Error happened during invoking action with Dispatcher. Error : ");
                    Debug.LogError(e);
                }
            }
        }
    }
}