using System;
using Godot;

namespace HeavenAbandoned.addons.Valossy.Controls.Lists;

[GlobalClass]
public partial class SelectableItemList : Control
{
    [Export]
    public int MaxSelected { get; set; } = 3;

    private Container container;
    private Callable callable;

    public SelectableItemList()
    {
       this.callable = new Callable(this, nameof(ItemOnToggled));
    }
    public override void _Ready()
    {
        base._Ready();

        this.container = GetChild(0) as Container;
        
        this.container.ChildEnteredTree += ContainerOnChildEnteredTree;
        
        this.container.ChildExitingTree += ContainerOnChildExitingTree;
    }

    private void ContainerOnChildEnteredTree(Node node)
    {
        if (node is SelectableItem item)
        {
            item.Connect(nameof(SelectableItem.Toggled), this.callable, (uint)ConnectFlags.AppendSourceObject);
        }
    }

    private void ItemOnToggled(bool toggledOn, SelectableItem item)
    {
        
    }

    private void ContainerOnChildExitingTree(Node node)
    {
        if (node is SelectableItem item)
        {
            item.Disconnect(nameof(SelectableItem.Toggled), this.callable);
        }
    }

    private void ItemOnPressed()
    {
        
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        this.container.ChildEnteredTree -= ContainerOnChildEnteredTree;
        
        this.container = null;
    }
}