using System;
using UnityEngine;
using Xo.LiquidFramework;

namespace Xo.Utilitiies
{
    /// <summary>
    /// GenericEvents are useful for handling large numbers of event.
    /// </summary>
    /// <typeparam name="T"> Type of events input parameter.</typeparam>
    /// <typeparam name="T1"> Type of events output parameter.</typeparam>
    [Serializable]
    public class GenericEvent<T,T1> where T : IEArgsInput where T1 : IEArgsOutput
    {
        [SerializeField] private string name;
        private int _hash;
        
        public delegate T1 EventDelegate(T args);
        private event EventDelegate MyEvent;
        
        public void Init()
        {
            _hash = name.GetHashCode(StringComparison.Ordinal);
        }
        
        /// <summary>
        /// Assign Method to GenericEvent
        /// </summary>
        /// <param name="method">Method which we want to assign.</param>
        /// <returns></returns>
        protected bool Assign(EventDelegate method)
        {
            MyEvent += method;
            return true;
        }

        /// <summary>
        /// Assign Method to GenericEvent with hash comparison.
        /// </summary>
        /// <param name="hash">Hash of event name.</param>
        /// <param name="method"> Method which we want to assign.</param>
        /// <returns> Is input hash same with events hash</returns>
        public bool TryToAssign(int hash,EventDelegate method)
        {
            return (_hash == hash) && Assign(method);
        }
        
        
        
        
        
        
        
        /// <summary>
        /// Remove specified method from invocation list.
        /// </summary>
        /// <param name="method">Method which we want to delete from invocation list.</param>
        /// <returns>Is removing successful? </returns>
        protected bool Remove(EventDelegate method)
        {
            if (MyEvent != null && MyEvent.GetInvocationList().Length > 0)
            {
                MyEvent -= method;
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Remove specified method from invocation list with hash comparison.
        /// </summary>
        /// <param name="hash">Hash of event name.</param>
        /// <param name="method">Method which we want to delete from invocation list.</param>
        /// <returns> Is input hash same with events hash && Is removing successful??</returns>
        public bool TryToRemove(int hash,EventDelegate method)
        {
            return (_hash == hash) && Remove(method);
        }
        
        
        
        
        
        
        
        /// <summary>
        /// Invoke event.
        /// </summary>
        /// <param name="ieArgsInput">Input for invoke process.</param>
        /// <param name="ieArgsOutput">Output reference for invoke process.</param>
        /// <returns>Is invoke successful?</returns>
        protected bool Invoke(T ieArgsInput, ref T1 ieArgsOutput)
        {
            if (MyEvent != null && MyEvent.GetInvocationList().Length > 0)
            {
                ieArgsOutput = MyEvent.Invoke(ieArgsInput);
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Invoke event with hash comparison.
        /// </summary>
        /// <param name="hash">Hash of event name.</param>
        /// <param name="ieArgsInput">Input for invoke process.</param>
        /// <param name="ieArgsOutput">Output reference for invoke process.</param>
        /// <returns>Is input hash same with events hash && Is invoke successful</returns>
        public bool TryToInvoke(int hash,T ieArgsInput, ref T1 ieArgsOutput)
        {
            return (_hash == hash) && Invoke(ieArgsInput,ref ieArgsOutput);
        }
        
        
        
        
        
        
        
        /// <summary>
        /// Clear GenericEvent.
        /// </summary>
        public void Clear()
        {
            if (MyEvent != null && MyEvent.GetInvocationList().Length > 0)
                foreach (Delegate e in MyEvent.GetInvocationList())
                {
                    MyEvent -= (EventDelegate)e;
                }
        }
        
        
        
    }
}
