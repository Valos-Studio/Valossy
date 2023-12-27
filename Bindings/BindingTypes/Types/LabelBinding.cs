using System;
using System.ComponentModel;
using Godot;

namespace Valossy.Bindings.BindingTypes.Types;

public class LabelBinding : IBindingTypeHandler
{
    public void Bind(object modelObject, string modelPropertyName, object viewObject, BindingMode bindingMode)
    {
        if (viewObject is not Label control)
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
    }

    public Type ProcessingType()
    {
        return typeof(Label);
    }

    private void SetValue(object modelObject, string modelPropertyName, Label control)
    {
        object value = modelObject.GetType().GetProperty(modelPropertyName).GetValue(modelObject);
        control.Text = value?.ToString();
    }
}