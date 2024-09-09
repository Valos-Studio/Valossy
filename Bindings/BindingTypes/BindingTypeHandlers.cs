using System;
using System.Collections.Generic;
using Valossy.Bindings.BindingTypes.Types;
using Valossy.Bindings.BindingTypes.Types.AutoCompletes;

namespace Valossy.Bindings.BindingTypes;

public static class BindingTypeHandlers
{
    private static readonly Dictionary<Type, IBindingTypeHandler>
        handlers = new Dictionary<Type, IBindingTypeHandler>();

    static BindingTypeHandlers()
    {
        AddBindingHandler(new LabelBinding());
        AddBindingHandler(new TextEditBinding());
        AddBindingHandler(new ContainerBinding());
        AddBindingHandler(new ProgressBarBinding());
        AddBindingHandler(new AutoCompleteItemsProvider());
        AddBindingHandler(new AutoCompleteSelectedItemBinding());
        AddBindingHandler(new TextureRectBinding());
    }

    public static IBindingTypeHandler GetBindingHandler(Type type)
    {
        handlers.TryGetValue(type, out IBindingTypeHandler handler);
        return handler;
    }

    private static void AddBindingHandler(IBindingTypeHandler handler)
    {
        handlers[handler.ProcessingType()] = handler;
    }
}
