using Godot;

namespace Valossy.Helpers.Areas;

public static class SquareCheck
{
    public static bool IsInside(Vector2 innerRectanglePosition, Vector2 innerRectangleSize, Vector2 outerRectanglePosition, Vector2 outerRectangleSize)
    {
        Vector2[] innerCorners = new Vector2[4]
        {
            // Top-left
            innerRectanglePosition,
            // Top-right
            new Vector2(innerRectanglePosition.X + innerRectangleSize.X, innerRectanglePosition.Y),
            // Bottom-left
            new Vector2(innerRectanglePosition.X, innerRectanglePosition.Y + innerRectangleSize.Y),
            // Bottom-right
            new Vector2(innerRectanglePosition.X + innerRectangleSize.X, innerRectanglePosition.Y + innerRectangleSize.Y)
        };

        foreach (Vector2 corner in innerCorners)
        {
            if (!IsPointInside(corner, outerRectanglePosition, outerRectangleSize))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsPointInside(Vector2 point, Vector2 positionB, Vector2 sizeB)
    {
        return point.X >= positionB.X &&
               point.X <= positionB.X + sizeB.X &&
               point.Y >= positionB.Y &&
               point.Y <= positionB.Y + sizeB.Y;
    }
}
