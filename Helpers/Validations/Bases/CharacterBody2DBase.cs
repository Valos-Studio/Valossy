using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using Valossy.Bindings;

namespace Valossy.Helpers.Validations.Bases;

public partial class CharacterBody2DBase : CharacterBody2D, INotifyPropertyChanged
{
    protected bool failedValidation;

    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    public override string[] _GetConfigurationWarnings()
    {
        string[] result = ValidationHelper.GetConfigurationWarnings(this);
        failedValidation = result.Any();
        return result;
    }

    public override void _Ready()
    {
        if (Engine.IsEditorHint() == false)
        {
            this.TreeEntered += OnTreeEntered;

            this.TreeExited += OnTreeExited;
        }
    }

    public void OnTreeEntered()
    {
        BindingHandler.ProcessBindings(this);
    }

    public void OnTreeExited()
    {
        BindingHandler.DisposeBindings(this);
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