using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityUtility
{
    /// <summary>
    /// Utility class to help find the nearest object of a given type.
    /// <example>
    /// <code>
    ///   DoorController nearestDoor = gameObject.position.GetNearestGameObject<DoorController>();
    /// </code>
    /// </example>
    /// </summary>
    public static class NearestGameObjectUtils
    {
        /// <summary>
        /// Returns the nearest GameObject of type T relative to a Vector3 position.
        /// Using Object.FindObjectsOfType<T>
        /// </summary>
        public static T GetNearestGameObject<T>(Vector3 position) where T : MonoBehaviour
        {
            return GetNearestGameObject(position, Object.FindObjectsOfType<T>().ToList());
        }

        /// <summary>
        /// Returns the nearest GameObject of type T relative to a List of objects.
        /// </summary>
        public static T GetNearestGameObject<T>(Vector3 myPosition, List<T> allObjs) where T : MonoBehaviour
        {
            T nearestObj = null;
            float minDist = float.MaxValue;
            
            foreach (T obj in allObjs)
            {
                float dist = Vector3.Distance(myPosition, obj.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearestObj = obj;
                }
            }
            
            return nearestObj;
        }
        
        // GetSortedListOfGameObjectByDistance
        public static List<T> GetSortedListOfGameObjectByDistance<T>(this Vector3 myPosition, List<T> allObjs) where T : MonoBehaviour
        {
            var sortedList = (from objects in allObjs let distance = Vector3.Distance(myPosition, objects.transform.position) select objects).ToList();
            sortedList.Sort((x, y) => Vector3.Distance(myPosition, x.transform.position).CompareTo(Vector3.Distance(myPosition, y.transform.position)));
            return sortedList;
        }
    }
}