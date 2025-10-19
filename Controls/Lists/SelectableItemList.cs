using System.ComponentModel;
using System.Runtime.CompilerServices;
using Godot;
using Container = Godot.Container;

namespace Valossy.Controls.Lists;

[GlobalClass]
public partial class SelectableItemList : Control, INotifyPropertyChanged
{
    [Export]
    public int MaxSelected
    {
        get => maxSelected;
        set
        {
            if (value == maxSelected) return;
            maxSelected = value;
            OnPropertyChanged();
        }
    }
    
    private Container container;
    private Callable callable;
    private int maxSelected = 3;

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

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}