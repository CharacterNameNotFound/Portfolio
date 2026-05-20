using UnityEngine;

namespace Game.GameMode.Session.Game.Utilities
{
    public static class CollisionChecks
    {
        public static bool IsCirclesCollided(Vector2 firstCenter, Vector2 secondCenter, float firstRadius, float secondRadius)
        {
            float fullRadius = firstRadius + secondRadius;
            float fullRadiusSquare = fullRadius * fullRadius;

            return (secondCenter - firstCenter).sqrMagnitude < fullRadiusSquare;
        }

        public static bool IsCircleBoxCollided(Vector2 circleCenter, Vector2 boxCenter, float circleRadius, Vector2 boxSize)
        {
            Vector2 circleDistance = new Vector2
            {
                x = Mathf.Abs(circleCenter.x - boxCenter.x),
                y = Mathf.Abs(circleCenter.y - boxCenter.y)
            };

            if (circleDistance.x > (boxSize.x / 2 + circleRadius))
            {
                return false;
            }

            if (circleDistance.y > (boxSize.y / 2 + circleRadius))
            {
                return false;
            }

            if (circleDistance.x <= (boxSize.x / 2))
            {
                return true;
            }

            if (circleDistance.y <= (boxSize.y / 2))
            {
                return true;
            }

            float cornerDistanceSqr = (circleDistance.x - boxSize.x / 2f) * (circleDistance.x - boxSize.x / 2f) +
                                      (circleDistance.y - boxSize.y / 2f) * (circleDistance.y - boxSize.y / 2f);

            return (cornerDistanceSqr <= (circleRadius * circleRadius));
        }
        
    }
}