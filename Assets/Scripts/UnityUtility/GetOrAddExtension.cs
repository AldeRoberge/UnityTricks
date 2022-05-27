using UnityEngine;

namespace UnityUtility
{
    public static class GetOrAddExtension
    {
        /// <summary>
        /// Gets or creates a Component of type T on the given GameObject.
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var existingComponent = gameObject.GetComponent<T>();
            return existingComponent != null ? existingComponent : gameObject.AddComponent<T>();
        }
    }
}