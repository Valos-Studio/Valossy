#if TOOLS
using Godot;
using Valossy.Cameras;

namespace Valossy;

[Tool]
public partial class ValossyPlugin : EditorPlugin
{
    public override void _EnterTree()
    {
        AddCustomTypes();
    }

    public override void _ExitTree()
    {
        RemoveCustomTypes();
    }
    
    private void AddCustomTypes()
    {
        // Script script = GD.Load<Script>("res://addons/Valossy/Cameras/ZoomCamera.cs");
        //
        // Texture2D texture = GD.Load<Texture2D>("res://addons/Valossy/Cameras/Camera.png");
        //
        // AddCustomType(nameof(ZoomCamera), "Camera2D", script, texture);
    }
    
    private void RemoveCustomTypes()
    {
        // RemoveCustomType(nameof(ZoomCamera));
    }
}
#endif