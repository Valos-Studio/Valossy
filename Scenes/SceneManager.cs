using Godot;

namespace Valossy.Scenes;

public partial class SceneManager : Node
{
    public void ChangeScene(PackedScene scene)
    {
        GetTree().ChangeSceneToPacked(scene);
    }

    public void ChangeScene(string scenePath)
    {
        GetTree().ChangeSceneToFile(scenePath);
    }
}