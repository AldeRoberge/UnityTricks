using UnityEngine;

namespace UnityUtility
{
    public class CircleUtils
    {
        // See https://www.redblobgames.com/grids/circle-drawing/
        public static bool IsInsideCircle(Vector2Int center, Vector2Int tile, float radius)
        {
            float dx = center.x - tile.x,
                dy = center.y - tile.y;

            float distance_squared = dx * dx + dy * dy;
            return distance_squared <= radius * radius;
        }
    }
}