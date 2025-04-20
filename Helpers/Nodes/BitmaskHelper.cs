using Godot;

namespace Valossy.Helpers.Nodes;

public static class BitmaskHelper
{
    /// <summary>
    /// Create a bitmask from list of positions
    /// </summary>
    /// <param name="bits">Bits are position to be set as 1, starting from 0</param>
    /// <returns></returns>
    public static uint CreateBitmask(params uint[] bits)
    {
        long bitmask = 0;

        foreach (uint position in bits)
        {
            bitmask |= (uint)(1 << (int)(position));
        }

        return (uint)bitmask;
    }

    /// <summary>
    /// Add a bit into specific position of a bitmask
    /// </summary>
    /// <param name="bitmask">Bitmask to be changed</param>
    /// <param name="position">Position Start from 0</param>
    /// <returns>Bitmask with added bit</returns>
    public static uint AddToBitmask(uint bitmask, uint position)
    {
        bitmask |= (uint)(1 << (int)(position));

        return bitmask;
    }

    /// <summary>
    /// Remove a bit from specific position of a bitmask
    /// </summary>
    /// <param name="bitmask">Bitmask to be changed</param>
    /// <param name="position">Position Start from 0</param>
    /// <returns>Bitmask with removed bit</returns>
    public static uint RemoveFromBitmask(uint bitmask, int position)
    {
        uint mask = ~(1u << position);

        return bitmask & mask;
    }
}
