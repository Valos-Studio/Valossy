using Godot;

namespace Valossy.Helpers.Areas;

public static class CircleCheck
{
    public static bool IsInside(Vector2 rectanglePosition, Vector2 rectangleSize, Vector2 circleCenter, float circleRadius)
    {
        Vector2[] innerCorners = new Vector2[4]
        {
            // Top-left
            rectanglePosition,
            // Top-right
            new Vector2(rectanglePosition.X + rectangleSize.X, rectanglePosition.Y),
            // Bottom-left
            new Vector2(rectanglePosition.X, rectanglePosition.Y + rectangleSize.Y),
            // Bottom-right
            new Vector2(rectanglePosition.X + rectangleSize.X, rectanglePosition.Y + rectangleSize.Y)
        };

        foreach (Vector2 corner in innerCorners)
        {
            if (IsPointInsideCircle(corner, circleCenter, circleRadius) == false)
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsPointInsideCircle(Vector2 point, Vector2 circleCenter, float radius)
    {
        return point.DistanceTo(circleCenter) <= radius;
    }
}
