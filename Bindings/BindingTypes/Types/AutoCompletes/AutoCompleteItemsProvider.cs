using System;
using System.Collections.Generic;
using System.Reflection;
using HeavenAbandoned.Framework.UserInterfaces.Controls.AutoCompletes;
using Valossy.Loggers;

namespace Valossy.Bindings.BindingTypes.Types.AutoCompletes;

public class AutoCompleteItemsProvider : IBindingTypeHandler
{
    public void Bind(object modelObject, string modelPropertyName, object viewObject, BindingMode bindingMode)
    {
        if (viewObject is not AutoComplete control)
        {
            Logger.Error(
                $"Invalid binding for {modelObject}.{modelPropertyName}. {viewObject} is not an {nameof(AutoComplete)}");
            return;
        }

        PropertyInfo property = modelObject.GetType().GetProperty(modelPropertyName);
        object propertyValue = property.GetValue(modelObject);

        if (propertyValue is Func<List<object>> itemsProvider)
        {
            control.ItemSourceProvider = itemsProvider;
        }
    }

    public Type ProcessingType()
    {
        return typeof(AutoCompleteItemsProvider);
    }
}