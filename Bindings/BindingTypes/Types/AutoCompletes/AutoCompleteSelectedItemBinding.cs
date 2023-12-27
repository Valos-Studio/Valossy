using System;
using System.ComponentModel;
using HeavenAbandoned.Framework.UserInterfaces.Controls.AutoCompletes;
using Valossy.Loggers;

namespace Valossy.Bindings.BindingTypes.Types.AutoCompletes;

public class AutoCompleteSelectedItemBinding : IBindingTypeHandler
{
    public void Bind(object modelObject, string modelPropertyName, object viewObject, BindingMode bindingMode)
    {
        if (viewObject is not AutoComplete control)
        {
            Logger.Error(
                $"Invalid binding for {modelObject}.{modelPropertyName}. {viewObject} is not an {nameof(AutoComplete)}");
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

        control.SelectedItemChanged += (selectedItem) =>
        {
            var property = modelObject.GetType().GetProperty(modelPropertyName);
            property?.SetValue(modelObject, selectedItem);
        };
    }

    public Type ProcessingType()
    {
        return typeof(AutoCompleteSelectedItemBinding);
    }

    private void SetValue(object modelObject, string modelPropertyName, AutoComplete control)
    {
        object value = modelObject.GetType().GetProperty(modelPropertyName).GetValue(modelObject);
        control.SelectedItem = value;
    }
}