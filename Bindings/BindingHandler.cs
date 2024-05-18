using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Valossy.Bindings.BindingTypes;
using Valossy.Loggers;

namespace Valossy.Bindings;

public class BindingHandler
{
    public static void ProcessBindings(object bindingObject)
    {
        PropertyInfo[] properties = bindingObject.GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            IEnumerable<Binding> attributes = property.GetCustomAttributes<Binding>();
            
            foreach (Binding attribute in attributes)
            {
                Type viewType = property.PropertyType;

                IBindingTypeHandler bindingTypeHandler =
                    BindingTypeHandlers.GetBindingHandler(attribute.BindingTypeHandler ?? viewType);

                if (bindingTypeHandler == null)
                {
                    Logger.Error($"No {nameof(BindingTypeHandlers)} found for {viewType}");
                    continue;
                }

                // Parent is needed for primitive types as we need to use .SetValue on the C# Property
                object parentObject = GetParentToBindingProperty(bindingObject, attribute.ModelPropertyPath);

                // We get the last property from the path as that is the binding property
                string modelPropertyName = attribute.ModelPropertyPath?.Split('.')?.Last();

                object viewObject = property.GetValue(bindingObject);

                if (viewObject == null)
                {
                    Logger.Error(
                        $"Binding failed, viewObject is not injected in the Inspector. Check the View injection for property {property.Name}");
                    continue;
                }

                if (parentObject == null)
                {
                    Logger.Error(
                        $"Binding failed, parentObject could not be found. Check the Binding for property {property.Name} and that the Model is perhaps not null.");
                    continue;
                }

                bindingTypeHandler.Bind(parentObject, modelPropertyName, viewObject, attribute.BindingMode);
            }
        }
    }

    public static object GetParentToBindingProperty(object bindingObject, string path)
    {
        Type currentType = bindingObject.GetType();

        string[] splitPath = path.Split('.');

        for (int i = 0; i < splitPath.Length; i++)
        {
            string propertyName = splitPath[i];

            PropertyInfo property = currentType.GetProperty(propertyName);
            
            if (property == null)
            {
                Logger.Error($"Failed to find the property on currentType {currentType.Name}. Available properties on type: ");
                
                foreach (PropertyInfo item in currentType.GetProperties())
                {
                    Logger.Error(item.Name);
                }

                return null;
            }

            // We check if we are one before last and then return if yes as we only want the parent
            if (i == splitPath.Length - 1)
            {
                break;
            }

            bindingObject = property.GetValue(bindingObject, null);

            if (bindingObject == null)
            {
                Logger.Error($"Failed to find the object value from property {property.Name}");
                return null;
            }

            currentType = bindingObject.GetType();
        }

        return bindingObject;
    }
    
    public static void DisposeBindings(object bindingObject)
    {
        PropertyInfo[] properties = bindingObject.GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            IEnumerable<Binding> attributes = property.GetCustomAttributes<Binding>();
            
            foreach (Binding attribute in attributes)
            {
                Type viewType = property.PropertyType;

                IBindingTypeHandler bindingTypeHandler =
                    BindingTypeHandlers.GetBindingHandler(attribute.BindingTypeHandler ?? viewType);

                if (bindingTypeHandler == null)
                {
                    Logger.Error($"No {nameof(BindingTypeHandlers)} found for {viewType}");
                    continue;
                }

                // Parent is needed for primitive types as we need to use .SetValue on the C# Property
                object parentObject = GetParentToBindingProperty(bindingObject, attribute.ModelPropertyPath);

                // We get the last property from the path as that is the binding property
                string modelPropertyName = attribute.ModelPropertyPath?.Split('.')?.Last();

                object viewObject = property.GetValue(bindingObject);

                if (viewObject == null)
                {
                    Logger.Error(
                        $"Binding failed, viewObject is not injected in the Inspector. Check the View injection for property {property.Name}");
                    continue;
                }

                if (parentObject == null)
                {
                    Logger.Error(
                        $"Binding failed, parentObject could not be found. Check the Binding for property {property.Name} and that the Model is perhaps not null.");
                    continue;
                }

                bindingTypeHandler.UnBind(parentObject, modelPropertyName, viewObject);
            }
        }
    }
}