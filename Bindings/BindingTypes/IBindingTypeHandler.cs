using System;

namespace Valossy.Bindings.BindingTypes;

public interface IBindingTypeHandler
{
    void Bind(object modelObject, string modelPropertyName, object viewObject, BindingMode bindingMode);

    Type ProcessingType();
}