using System;
using System.ComponentModel;
using Godot;

namespace Valossy.Bindings.BindingTypes.Types;

public class TextEditBinding : IBindingTypeHandler
{
    public void Bind(object modelObject, string modelPropertyName, object viewObject, BindingMode bindingMode)
    {
        if (viewObject is not TextEdit control)
        {
            return;
        }

        if (modelObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            // Set the initial value
            this.SetValue(modelObject, modelPropertyName, control);

            notifyPropertyChanged.PropertyChanged += (sender, e) =>
            {
                if (Equals(e.PropertyName, modelPropertyName))
                {
                    this.SetValue(modelObject, modelPropertyName, control);
                }
            };
        }

        if (BindingMode.OneWay.Equals(bindingMode))
        {
            // Just return here since we don't do two way bind
            return;
        }

        control.TextChanged += () =>
        {
            var property = modelObject.GetType().GetProperty(modelPropertyName);
            property?.SetValue(modelObject, control.Text);
        };
    }

    public void UnBind(object modelObject, string modelPropertyName, object viewObject)
    {
        throw new NotImplementedException();
    }

    public Type ProcessingType()
    {
        return typeof(TextEdit);
    }

    private void SetValue(object modelObject, string modelPropertyName, TextEdit control)
    {
        object value = modelObject.GetType().GetProperty(modelPropertyName).GetValue(modelObject);
        control.Text = value?.ToString();

        if (control.Text != null)
        {
            control.SetCaretColumn(control.Text.Length);
        }
    }
}