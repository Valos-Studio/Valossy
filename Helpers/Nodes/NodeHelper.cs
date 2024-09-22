using Godot;

namespace Valossy.Helpers.Nodes;

public static class NodeHelper
{
    public static void ReparentNode(Node node, Node newParentNode)
    {
        if (node == null || newParentNode == null)
        {
            return;
        }

        if (node.GetParent() == null)
        {
            newParentNode.AddChild(node);
        }
        else
        {
            node.Reparent(newParentNode);
        }

        if (node is Control { Visible: false } controlNode)
        {
            controlNode.Visible = true;
        }
    }

    /// <summary>
    /// This will make sure the node is not null or QueueFree or in some process of being disposed
    /// Usage this.IsValid&lt;ClassName&gt;()
    /// </summary>
    /// <returns>True if the object is safe for usage</returns>
    public static bool IsValid<T>(T node) where T : GodotObject
    {
        return node != null
               && GodotObject.IsInstanceValid(node)
               && !node.IsQueuedForDeletion();
    }
}
