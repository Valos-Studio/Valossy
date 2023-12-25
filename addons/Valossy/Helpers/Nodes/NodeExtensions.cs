using Godot;

namespace Valossy.Helpers.Nodes;

public static class NodeExtensions
{
    public static T GetGlobalNode<T>(this Node node) where T : class
    {
        return node.GetNode<T>($"/root/{typeof(T).Name}");
    }

    public static bool IsValid<T>(this T node) where T : GodotObject
    {
        return node != null
               && GodotObject.IsInstanceValid(node)
               && !node.IsQueuedForDeletion();
    }
}