using System;
using System.Collections.Generic;
using System.ComponentModel;
using Godot;

namespace Valossy.Bindings.BindingTypes.Types;

public class LabelBinding : IBindingTypeHandler
{
    private readonly Dictionary<string, PropertyChangedEventHandler> delegates;

    public LabelBinding()
    {
        delegates = new Dictionary<string, PropertyChangedEventHandler>();
    }

    public void Bind(object modelObject, string modelPropertyName, object viewObject, BindingMode bindingMode)
    {
        if (viewObject is not Label control)
        {
            return;
        }

        if (modelObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            string uniqueName = CreateUniqueName(modelObject, modelPropertyName, control);

            if (delegates.ContainsKey(uniqueName) == true)
            {
                //Already subscribed

                return;
            }

            PropertyChangedEventHandler handler = ChangedOnPropertyChanged;

            // Set the initial value
            this.SetValue(modelObject, modelPropertyName, control);

            delegates.Add(uniqueName, handler);

            notifyPropertyChanged.PropertyChanged += handler;
        }

        void ChangedOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Equals(e.PropertyName, modelPropertyName))
            {
                this.SetValue(modelObject, modelPropertyName, control);
            }
        }
    }

    public void UnBind(object modelObject, string modelPropertyName, object viewObject)
    {
        if (viewObject is not Label control)
        {
            return;
        }

        string uniqueName = CreateUniqueName(modelObject, modelPropertyName, control);

        bool exists = this.delegates.TryGetValue(uniqueName, out PropertyChangedEventHandler handler);

        if (exists == false)
        {
            //Nothing to unsubscribe

            return;
        }

        if (modelObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged -= handler;
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

    private string CreateUniqueName(object modelObject, string modelPropertyName, Control viewObject)
    {
        return $"{modelObject.GetHashCode()}{modelPropertyName}{viewObject.GetInstanceId()}";
    }
}