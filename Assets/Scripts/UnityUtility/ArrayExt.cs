namespace UnityUtility
{
    public static class ArrayExt
    {
        // Returns a random object from the array
        public static T Random<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }
    }
}