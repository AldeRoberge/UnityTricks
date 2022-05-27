using UnityEngine;

namespace UnityUtility
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// Sets the layer of the game object and all its children.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="layer"></param>
        /// <param name="includeChildren"></param>
        public static void SetLayer(this GameObject parent, int layer, bool includeChildren = true)
        {
            parent.layer = layer;

            if (!includeChildren) return;

            foreach (var transform in parent.transform.GetComponentsInChildren<Transform>(true))
            {
                transform.gameObject.layer = layer;
            }
        }
    }
}