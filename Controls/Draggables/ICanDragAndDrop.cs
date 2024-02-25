using Godot;

namespace HeavenAbandoned.Framework.UserInterfaces.Controls.DragAndDrops;

public interface ICanDragAndDrop
{
    public Texture2D PreviewTexture { get; }
    public Control DragAndDropPreview();
}