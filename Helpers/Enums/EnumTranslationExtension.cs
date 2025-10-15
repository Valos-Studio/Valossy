using System;
using System.ComponentModel;

namespace Valossy.Helpers.Enums;

public static class EnumTranslationExtension
{
    public static string GetTranslationString(this Enum val)
    {
        TranslationAttribute[] attributes = (TranslationAttribute[])val
            .GetType()
            .GetField(val.ToString())
            .GetCustomAttributes(typeof(TranslationAttribute), false);
        return attributes.Length > 0 ? attributes[0].Translation : string.Empty;
    }
} 