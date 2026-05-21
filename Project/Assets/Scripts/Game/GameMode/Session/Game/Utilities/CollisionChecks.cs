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

        public static bool IsCircleBoxCollided(Vector2 circleCenter, Vector2 boxCenter, float circleRadius, Vector2 boxExtends)
        {
            Vector2 circleDistance = new Vector2
            {
                x = Mathf.Abs(circleCenter.x - boxCenter.x),
                y = Mathf.Abs(circleCenter.y - boxCenter.y)
            };

            if (circleDistance.x > (boxExtends.x + circleRadius))
            {
                return false;
            }

            if (circleDistance.y > (boxExtends.y + circleRadius))
            {
                return false;
            }

            if (circleDistance.x <= (boxExtends.x ))
            {
                return true;
            }

            if (circleDistance.y <= (boxExtends.y))
            {
                return true;
            }

            float cornerDistanceSqr = (circleDistance.x - boxExtends.x) * (circleDistance.x - boxExtends.x) +
                                      (circleDistance.y - boxExtends.y) * (circleDistance.y - boxExtends.y);

            return (cornerDistanceSqr <= (circleRadius * circleRadius));
        }
        
        public static bool IsCircleRotatedBoxCollided(Vector2 circleCenter, float radius, Vector2 rectCenter, Vector2 rectSize, float rotationRadians)
        {
            float cos = Mathf.Cos(-rotationRadians);
            float sin = Mathf.Sin(-rotationRadians);
    
            Vector2 relativePos = circleCenter - rectCenter;
            Vector2 localCirclePos = new Vector2(
                relativePos.x * cos - relativePos.y * sin,
                relativePos.x * sin + relativePos.y * cos
            );

            float halfWidth = rectSize.x / 2;
            float halfHeight = rectSize.y / 2;

            float closestX = Mathf.Clamp(localCirclePos.x, -halfWidth, halfWidth);
            float closestY = Mathf.Clamp(localCirclePos.y, -halfHeight, halfHeight);

            float distanceX = localCirclePos.x - closestX;
            float distanceY = localCirclePos.y - closestY;

            return (distanceX * distanceX) + (distanceY * distanceY) <= (radius * radius);
        }
        
        
        
    }
}