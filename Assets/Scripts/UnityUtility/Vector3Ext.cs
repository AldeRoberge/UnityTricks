using UnityEngine;

namespace UnityUtility
{
    public static class Vector3Ext
    {
        // Clone a Vector3
        public static Vector3 Clone(this Vector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }
    }
}