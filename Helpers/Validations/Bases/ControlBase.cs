using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using Valossy.Bindings;
using Valossy.Controls.Draggables;
using Valossy.Loggers;

namespace Valossy.Helpers.Validations.Bases;

public partial class ControlBase : Control, INotifyPropertyChanged
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

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        if (this is ICanBeDroppedInto canBeDroppedInto)
        {
            var droppedControl = data.As<Node>();
            return canBeDroppedInto.CanDropDataIn(droppedControl);
        }

        return false;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        Node controlToDropDataIn = data.As<Node>();

        if (this is ICanBeDroppedInto canBeDroppedInto && controlToDropDataIn is ICanDragAndDrop canDragAndDrop)
        {
            Logger.Trace(
                $"Dropping {controlToDropDataIn?.GetType().Name} into control {canBeDroppedInto?.GetType().Name}");
            canBeDroppedInto.ProcessDraggedItem(canDragAndDrop);
        }
    }

    protected override void Dispose(bool disposing)
    {
        Delegate[] invocationList = PropertyChanged?.GetInvocationList();

        if (invocationList != null)
        {
            foreach (Delegate d in invocationList)
            {
                PropertyChanged -= (d as PropertyChangedEventHandler);
            }
        }

        base.Dispose(disposing);
    }
}