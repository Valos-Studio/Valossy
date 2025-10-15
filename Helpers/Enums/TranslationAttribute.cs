using System;

namespace Valossy.Helpers.Enums;

[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
public sealed class TranslationAttribute : Attribute
{
    public string Translation { get; }

    public TranslationAttribute(string translation)
    {
        Translation = translation;
    }
}