using Godot;

namespace Valossy.Controls.Lists;

[GlobalClass]
public partial class SelectableItem : BaseButton
{
    public override void _Ready()
    {
       base._Ready();
       
       ToggleMode = true;
    }
}