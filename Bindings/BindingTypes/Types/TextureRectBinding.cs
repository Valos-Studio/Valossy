using System;
using System.ComponentModel;
using Godot;

namespace Valossy.Bindings.BindingTypes.Types;

public class TextureRectBinding : IBindingTypeHandler
{
    public void Bind(object modelObject, string modelPropertyName, object viewObject, BindingMode bindingMode)
    {
        if (viewObject is not TextureRect control)
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
    }

    public Type ProcessingType()
    {
        return typeof(TextureRect);
    }

    private void SetValue(object modelObject, string modelPropertyName, TextureRect control)
    {
        object value = modelObject.GetType().GetProperty(modelPropertyName).GetValue(modelObject);
        control.Texture = value as Texture2D;
    }
}