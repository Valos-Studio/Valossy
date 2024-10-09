namespace Valossy.Helpers.Nodes;

public static class BitmaskHelper
{
    public static uint CreateBitmask(params uint[] bits)
    {
        long bitmask = 0;

        foreach (uint position in bits)
        {
            bitmask |= (uint)(1 << (int)(position - 1));
        }

        return (uint)bitmask;
    }
}
