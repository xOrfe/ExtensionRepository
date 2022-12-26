using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Xo.LiquidFramework
{
    /// <summary>
    /// Simple base scriptable object for definition of ExtensionRepository.
    /// *** Extensions are much more powerful when you use with scriptable objects and editor tool. ***
    /// </summary>
    [CreateAssetMenu(fileName = "ExtensionRepositoryDefinition", menuName = "xOrfe/ExtensionRepositoryDefinition", order = 0)]
    public class ExtensionRepositoryDefinition : ScriptableObject
    {
        /// <summary>
        /// List of extension.
        /// "SerializableInterface" used for exposing interfaces.
        /// </summary>
        public List<SerializableInterface<IExtension>> extensions;
    }
}

    