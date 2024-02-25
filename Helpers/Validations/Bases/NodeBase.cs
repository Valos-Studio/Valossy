using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using HeavenAbandoned.Framework.UserInterfaces.Controls.DragAndDrops;
using Valossy.Bindings;
using Valossy.Loggers;

namespace HeavenAbandoned.Framework.Helpers.Validation.ValidationBase;

public partial class NodeBase : Node, INotifyPropertyChanged
{
    protected bool failedValidation;

    public event PropertyChangedEventHandler PropertyChanged;

    public event EventHandler ControlSelected;

    public void RaisePropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    public void OnControlSelected()
    {
        ControlSelected?.Invoke(this, EventArgs.Empty);
    }

    public override string[] _GetConfigurationWarnings()
    {
        string[] result = ValidationHelper.GetConfigurationWarnings(this);
        failedValidation = result.Any();
        return result;
    }

    public override void _Ready()
    {
        if (Engine.IsEditorHint() == true)
        {
        }
        else
        {
            BindingHandler.ProcessBindings(this);
        }
    }

    public bool _CanDropData(Vector2 atPosition, Variant data)
    {
        if (this is ICanBeDroppedInto canBeDroppedInto)
        {
            Node droppedControl = data.As<Node>();

            return canBeDroppedInto.CanDropDataIn(droppedControl);
        }

        return false;
    }

    public void _DropData(Vector2 atPosition, Variant data)
    {
        Node controlToDropDataIn = data.As<Node>();

        if (this is ICanBeDroppedInto canBeDroppedInto && controlToDropDataIn is ICanDragAndDrop canDragAndDrop)
        {
            Logger.Trace(
                $"Dropping {controlToDropDataIn?.GetType().Name} into control {canBeDroppedInto?.GetType().Name}");
            canBeDroppedInto.ProcessDraggedItem(canDragAndDrop);
        }
    }
}