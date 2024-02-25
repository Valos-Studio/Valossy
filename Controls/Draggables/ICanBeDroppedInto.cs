using Godot;

namespace HeavenAbandoned.Framework.UserInterfaces.Controls.DragAndDrops
{
    public interface ICanBeDroppedInto
    {
        bool CanDropDataIn(Node droppedControl);
        void ProcessDraggedItem(ICanDragAndDrop canDragAndDrop);
    }
}