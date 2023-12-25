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
}