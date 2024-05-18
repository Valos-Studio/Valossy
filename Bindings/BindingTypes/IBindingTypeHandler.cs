using System;

namespace Valossy.Bindings.BindingTypes;

public interface IBindingTypeHandler
{
    void Bind(object modelObject, string modelPropertyName, object viewObject, BindingMode bindingMode);

    void UnBind(object modelObject, string modelPropertyName, object viewObject);

    Type ProcessingType();
}