using System;
using System.Collections;
using System.Collections.Specialized;
using Godot;
using Valossy.Collections;
using Valossy.Controls.Generals;
using Valossy.Helpers.Nodes;

namespace Valossy.Bindings.BindingTypes.Types;

public class ContainerBinding : IBindingTypeHandler
{
    public void Bind(object modelObject, string modelPropertyName, object viewObject, BindingMode bindingMode)
    {
        if (viewObject is not Container control)
        {
            return;
        }

        object value = modelObject.GetType().GetProperty(modelPropertyName)?.GetValue(modelObject);
        if (value is IBindingCollection list)
        {
            var modelObjectNode = modelObject as Node;
            foreach (var item in list.GetItemsSafe())
            {
                if (item is Node node)
                {
                    NodeHelper.ReparentNode(node, control);
                }
            }
        }

        if (value is INotifyCollectionChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.CollectionChanged += (sender, e) =>
            {
                if (NotifyCollectionChangedAction.Add == e.Action && e.NewItems != null)
                {
                    foreach (var item in e.NewItems)
                    {
                        if (item is Node node)
                        {
                            NodeHelper.ReparentNode(node, control);
                        }
                    }
                }

                if (NotifyCollectionChangedAction.Remove == e.Action && e.OldItems != null)
                {
                    foreach (var item in e.OldItems)
                    {
                        if (item is Node node)
                        {
                            node.CallDeferred(Control.MethodName.QueueFree);
                        }
                    }
                }
            };
        }

        var property = modelObject.GetType().GetProperty(modelPropertyName).GetValue(modelObject);

        control.ChildEnteredTree += (child) =>
        {
            if (property is IList collection)
            {
                if (child is ICanBeSelected canBeSelected && collection is IListenToSelected listenToSelected)
                {
                    canBeSelected.ControlSelected += listenToSelected.SelectedItemChanged;
                }
            }
        };

        control.ChildExitingTree += (child) =>
        {
            if (property is IList collection)
            {
                if (child is ICanBeSelected canBeSelected && collection is IListenToSelected listenToSelected)
                {
                    canBeSelected.ControlSelected -= listenToSelected.SelectedItemChanged;
                }
            }
        };
    }

    public void UnBind(object modelObject, string modelPropertyName, object viewObject)
    {
        throw new NotImplementedException();
    }

    public Type ProcessingType()
    {
        return typeof(Container);
    }
}