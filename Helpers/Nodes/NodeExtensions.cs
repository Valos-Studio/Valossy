using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace Valossy.Helpers.Nodes;

public static class NodeExtensions
{
    /// <summary>
    /// Usage this.GetGlobalNode&lt;ClassName&gt;()
    /// </summary>
    /// <typeparam name="T">Name must be same name as Class</typeparam>
    /// <returns>Object from Autoload</returns>
    public static T GetGlobalNode<T>(this Node node) where T : class
    {
        return node.GetNode<T>($"/root/{typeof(T).Name}");
    }

    /// <summary>
    /// Get node in group
    /// </summary>
    /// <typeparam name="T">Group must be same name as Class</typeparam>
    /// <returns>First object from group</returns>
    public static T GetFirstNodeInGroup<T>(this Node node) where T : class
    {
        Array<Node> nodes = node.GetTree().GetNodesInGroup($"{typeof(T).Name}");

        return nodes[0] as T;
    }

    /// <summary>
    /// This will make sure the node is not null or QueueFree or in some process of being disposed
    /// Usage this.IsValid&lt;ClassName&gt;()
    /// </summary>
    /// <returns>True if the object is safe for usage</returns>
    public static bool IsValid<T>(this T node) where T : GodotObject
    {
        return node != null
               && GodotObject.IsInstanceValid(node)
               && !node.IsQueuedForDeletion();
    }

    public static async Task WaitNextFrame(this Node node)
    {
        await node.ToSignal(node.GetTree(), "process_frame");
    }

    public static void AddToInterfaceGroup<T>(this Node node) where T : class
    {
        if (Engine.IsEditorHint() == false)
        {
            node.AddToGroup(typeof(T).Name);
        }
    }

    public static string Translate(this Node node, string text, params object[] args)
    {
        string translation = node.Tr(text);

        string[] items = new string[args.Length];

        for (int index = 0; index < args.Length; index++)
        {
            object arg = args[index];

            string result;

            if (arg is string str)
            {
                result = node.Tr(str);
            }
            else
            {
                result = arg.ToString();
            }

            items[index] = result;
        }

        return String.Format(translation, items);
    }
}
