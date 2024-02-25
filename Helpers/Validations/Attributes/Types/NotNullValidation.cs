using System;
using System.Reflection;

namespace Valossy.Helpers.Validations.Attributes.Types;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class NotNullValidation : Attribute, IValidationAttribute
{
    public string ErrorMessage { get; set; }

    public Attribute ActionAttribute { get; set; }

    public ValidationResult Validate(PropertyInfo property, object validationObject)
    {
        ValidationResult validationResult = ValidationResult.SuccessfulResult;
        object value = property.GetValue(validationObject);
        if (value == null)
        {
            validationResult = ValidationResult.FailedResult($"{property.Name} is not set");
        }

        return validationResult;
    }
}