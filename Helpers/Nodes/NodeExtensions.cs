using System.Threading.Tasks;
using Godot;

namespace Valossy.Helpers.Nodes;

public static class NodeExtensions
{
    /// <summary>
    /// Usage this.GetGlobalNode&lt;ClassName&gt;()  
    /// </summary>
    /// <typeparam name="T">Class</typeparam>
    /// <returns>Object from Autoload</returns>
    public static T GetGlobalNode<T>(this Node node) where T : class
    {
        return node.GetNode<T>($"/root/{typeof(T).Name}");
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
}