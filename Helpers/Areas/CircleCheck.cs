using Godot;

namespace Valossy.Helpers.Areas;

public static class CircleCheck
{
    /// <summary>
    /// This works for rectangles that have their starting point on the left upper corner
    /// </summary>
    /// <returns>Returns true when the rectangle is inside of a circle</returns>
    public static bool IsInside(Vector2 rectanglePosition, Vector2 rectangleSize, Vector2 circleCenter,
        float circleRadius)
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

    /// <summary>
    /// This works for rectangles that have their starting point centered in the middle
    /// </summary>
    /// <returns>Returns true when the rectangle is inside of a circle</returns>
    public static bool IsInsideCentered(Vector2 rectanglePosition, Vector2 rectangleSize, Vector2 circleCenter,
        float circleRadius)
    {
        rectangleSize /= 2;

        Vector2[] innerCorners = new Vector2[4]
        {
            // Top-left
            new Vector2(rectanglePosition.X - rectangleSize.X, rectanglePosition.Y + rectangleSize.Y),
            // Top-right
            new Vector2(rectanglePosition.X + rectangleSize.X, rectanglePosition.Y + rectangleSize.Y),
            // Bottom-left
            new Vector2(rectanglePosition.X - rectangleSize.X, rectanglePosition.Y - rectangleSize.Y),
            // Bottom-right
            new Vector2(rectanglePosition.X + rectangleSize.X, rectanglePosition.Y - rectangleSize.Y)
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
