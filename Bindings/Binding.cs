using System;

namespace Valossy.Bindings;

[AttributeUsage(
    AttributeTargets.Property |
    AttributeTargets.Field,
    AllowMultiple = true)]
public class Binding : Attribute
{
    public Binding(string modelPropertyPath, string viewPropertyPath = null,
        BindingMode bindingMode = BindingMode.TwoWay, Type bindingTypeHandler = null)
    {
        this.ViewPropertyPath = viewPropertyPath;
        this.ModelPropertyPath = modelPropertyPath;
        this.BindingMode = bindingMode;
        this.BindingTypeHandler = bindingTypeHandler;
    }

    public Binding(Type bindingTypeHandler, string modelPropertyPath) : this(modelPropertyPath, null,
        BindingMode.TwoWay, bindingTypeHandler)
    {
    }

    public Binding(string modelPropertyPath, BindingMode bindingMode) : this(modelPropertyPath, null, bindingMode, null)
    {
    }

    public Binding(string modelPropertyPath, string viewPropertyPath) : this(modelPropertyPath, viewPropertyPath,
        BindingMode.TwoWay, null)
    {
    }

    public string ViewPropertyPath { get; }

    public string ModelPropertyPath { get; }

    public BindingMode BindingMode { get; }

    public Type BindingTypeHandler { get; }
}