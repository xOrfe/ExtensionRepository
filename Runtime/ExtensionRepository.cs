using System;
using System.Collections.Generic;
using TNRD;
using UnityEngine;
using Xo.Utilitiies;

namespace Xo.LiquidFramework
{
    public abstract class ExtensionRepository : MonoBehaviour
    {
        [SerializeField] private List<GenericEvent<IEArgsInput, IEArgsOutput>> events;
        [SerializeField] private ExtensionRepositoryDefinition extensionRepositoryDefinition;

        private bool _isInitialized;
        public bool IsInitialized => _isInitialized;
        
        private List<IExtension> _extensions;
        
        private void Awake()
        {
            _isInitialized = false;
            if(extensionRepositoryDefinition != null) Init(extensionRepositoryDefinition);
        }
        
        /// <summary>
        /// Method for extensions to add themselves to specified events
        /// </summary>
        /// <param name="eventName"> Events name</param>
        /// <param name="method"> Method we want to assign </param>
        public void AssignExtensionToEvent(string eventName, GenericEvent<IEArgsInput, IEArgsOutput>.EventDelegate method)
        {
            var hash = eventName.GetHashCode(StringComparison.Ordinal);

            foreach (var eEvent in events)
            {
                if (eEvent.TryToAssign(hash, method)) return;
            }
        }
        
        /// <summary>
        /// Invokes repository's specified event with string.
        /// </summary>
        /// <param name="eventName">Event name we want to invoke</param>
        /// <param name="ieArgsInput"> Input arguments </param>
        /// <param name="ieArgsOutput"> Output arguments </param>
        /// <returns> Is invoke successful? </returns>
        public bool InvokeEvent(string eventName, IEArgsInput ieArgsInput, ref IEArgsOutput ieArgsOutput)
        {
            var hash = eventName.GetHashCode(StringComparison.Ordinal);
            
            return InvokeEvent(hash,ieArgsInput,ref ieArgsOutput);
        }
        
        /// <summary>
        /// Invokes repository's specified event with hash.
        /// </summary>
        /// <param name="eventHash"> Event name we want to invoke </param>
        /// <param name="ieArgsInput"> Input arguments </param>
        /// <param name="ieArgsOutput"> Output arguments </param>
        /// <returns> Is invoke successful? </returns>
        public bool InvokeEvent(int eventHash, IEArgsInput ieArgsInput, ref IEArgsOutput ieArgsOutput)
        {
            foreach (var eEvent in events)
                if (eEvent.TryToInvoke(eventHash, ieArgsInput, ref ieArgsOutput))
                    return true;
            
            return false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="extensionRepositoryDefinition"></param>
        private void Init(ExtensionRepositoryDefinition extensionRepositoryDefinition)
        {
            _extensions = new List<IExtension>();
            
            foreach (var eEvent in events)
            {
                eEvent.Init();
            }
            
            foreach (var extension in this.extensionRepositoryDefinition.extensions)
            {
                _extensions.Add(extension.Value);
                _extensions[^1].Init(this);
            }
            
            _isInitialized = true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void Clear()
        {
            _isInitialized = false;
            _extensions.Clear();
        }
        
    }
}