using Godot;

namespace Valossy.Controls.Draggables;

public interface ICanBeDroppedInto
{
    bool CanDropDataIn(Node droppedControl);
    void ProcessDraggedItem(ICanDragAndDrop canDragAndDrop);
}