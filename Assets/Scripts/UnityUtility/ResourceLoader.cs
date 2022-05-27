using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Utils.Resource
{
    /// <summary>
    /// A fail safe way to load resources.
    /// Will return a fall back object if the resource is not found.
    ///
    /// 1. Create a folder at : Resources/_Fallback/
    /// 2. Add your fallback prefabs, textures and other resources.
    /// 3. Use ResourceLoader.LoadResource<T>("Path/To/Real/Resource")
    /// 4. ResourceLoader will return the fallback resource if the real resource is not found.
    /// </summary>
    public static class ResourceLoader
    {
        private static readonly Dictionary<string, Object> CachedObjects = new Dictionary<string, Object>();
        private static readonly Dictionary<string, List<Object>> CachedObjectsAll = new Dictionary<string, List<Object>>();

        /// <summary>
        /// Loads a single resource of the given type.
        /// If tryFallback is true, will return a fallback object if the resource is not found.
        /// </summary>
        public static T Load<T>(string path, bool tryFallback = true, bool loadFromCache = true, bool logMissing = true) where T : Object
        {
            if (string.IsNullOrEmpty(path))
            {
                PrintPathForResourceIsNull();
            }
            else
            {
                // Attempt to load from cache
                if (loadFromCache && CachedObjects.ContainsKey(path))
                {
                    return (T)CachedObjects[path];
                }

                var resource = Resources.Load<T>(path);

                // If the resource exists
                if (resource != null)
                {
                    // Load into cache and return
                    CachedObjects[path] = resource;
                    return resource;
                }

                if (logMissing) PrintNoResourceAtPath(path);
            }

            if (tryFallback)
                return ResourcesFallback.GetFallback<T>() as T;

            return default;
        }

        /// <summary>
        /// Loads all resources of the given type.
        /// If tryFallback is true, will return a fallback object if the resource is not found.
        /// </summary>
        public static T[] LoadAll<T>(string path, bool tryFallback = true, bool loadFromCache = true, bool logMissing = true) where T : Object
        {
            if (string.IsNullOrEmpty(path))
            {
                PrintPathForResourceIsNull();
            }
            else
            {
                // Attempt to load from cache
                if (loadFromCache && CachedObjectsAll.ContainsKey(path))
                {
                    return CachedObjectsAll[path].ToArray() as T[];
                }

                var resource = Resources.LoadAll<T>(path);

                // The resource exists
                if (resource != null)
                {
                    // Load into cache and return
                    CachedObjectsAll[path] = new List<Object>(resource);
                    return resource;
                }

                if (logMissing) PrintNoResourceAtPath(path);
            }

            if (tryFallback)
                return ResourcesFallback.GetFallbackAll<T>() as T[];

            return default;
        }

        private static void PrintPathForResourceIsNull()
        {
            Debug.LogError("[ResourceLoader] You are trying to load a resource, but the path is null or empty.");
        }

        private static void PrintNoResourceAtPath(string path)
        {
            Debug.LogError(
                "[ResourceLoader] Resource loaded from '" + path + "' is null.\n" +
                "Possible causes : The file extension is included in the resource file path or there is no resource at the given path.");
        }
    }

    /// <summary>
    /// Provides an utility mapping of Object types to 'fallback' resources.
    /// </summary>
    public static class ResourcesFallback
    {
        private static readonly GameObject gameObject;
        private static readonly Material material;
        private static readonly AudioClip audioClip;
        private static readonly Sprite sprite;

        static ResourcesFallback()
        {
            gameObject = ResourceLoader.Load<GameObject>("_Fallback/GameObject", false);
            material = ResourceLoader.Load<Material>("_Fallback/Material", false);
            audioClip = ResourceLoader.Load<AudioClip>("_Fallback/AudioClip", false);
            sprite = ResourceLoader.Load<Sprite>("_Fallback/Sprite", false);
        }

        public static IEnumerable<Object> GetFallbackAll<T>()
        {
            return new[] { GetFallback<T>() };
        }

        public static Object GetFallback<T>()
        {
            if (typeof(T).IsAssignableFrom(typeof(GameObject)))
                return gameObject;

            if (typeof(T).IsAssignableFrom(typeof(Material)))
                return material;

            if (typeof(T).IsAssignableFrom(typeof(AudioClip)))
                return audioClip;

            if (typeof(T).IsAssignableFrom(typeof(Sprite)))
                return sprite;

            Debug.LogError("[ResourcesFallback] No fall back resource for type '" + typeof(T) + "'.");
            return default;
        }
    }
}