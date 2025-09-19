
using System;
using Godot;
using Godot.Collections;

namespace Valossy.Helpers.Nodes;

public static class TileMapLayerExtensions
{
    /// <summary>
    /// Computes the world bounds of a TileMapLayer by analyzing its used cells.
    /// </summary>
    /// <param name="tileMapLayer">The TileMapLayer instance to calculate the bounds for.</param>
    /// <returns>A Rect2 representing the world bounds of the TileMapLayer in global coordinates.</returns>
    /// <exception cref="Exception">Thrown when no cells are used in the TileMapLayer.</exception>
    public static Rect2 GetWorldBounds(this TileMapLayer tileMapLayer)
    {
        Array<Vector2I> usedCells = tileMapLayer.GetUsedCells();
        
        if (usedCells.Count == 0) throw new Exception("No cells used in TileMapLayer");

        Vector2 min = usedCells[0];
        Vector2 max = usedCells[0];

        foreach (Vector2 cell in usedCells)
        {
            min = new Vector2(Mathf.Min(min.X, cell.X), Mathf.Min(min.Y, cell.Y));
            max = new Vector2(Mathf.Max(max.X, cell.X), Mathf.Max(max.Y, cell.Y));
        }

        Vector2 sizeInCells = max - min + Vector2.One; // include last cell
        
        Vector2 sizeInWorld = sizeInCells * tileMapLayer.TileSet.TileSize;
        Vector2 positionInWorld = min * tileMapLayer.TileSet.TileSize;

        return new Rect2(positionInWorld, sizeInWorld);
    }
}