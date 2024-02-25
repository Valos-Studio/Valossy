using Godot;

namespace Valossy.Controls.Draggables;

public interface ICanDragAndDrop
{
    public Texture2D PreviewTexture { get; }
    public Control DragAndDropPreview();
}