using UnityEngine;

namespace WordMaster
{
    public static class LevelExtensions
    {
        public static Vector2Int EnsurePosition(this Level level, Vector2Int position)
        {
            if (level.UsedPositions.Contains(position))
            {
                var offset = Random.value > .5f ? Vector2Int.right : Vector2Int.left;
                return position + offset * 2;
            }

            return position;
        }

        public static bool IsColliding(this Level level, Vector2Int position, float radius)
        {
            foreach (var usedPosition in level.UsedPositions)
            {
                if ((usedPosition - position).magnitude < radius)
                    return true;
            }

            return false;
        }
    }
}