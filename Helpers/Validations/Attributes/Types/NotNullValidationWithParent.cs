using System;
using System.Reflection;
using Godot;

namespace Valossy.Helpers.Validations.Attributes.Types;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class NotNullValidationWithParent : Attribute, IValidationAttribute
{
    public string ErrorMessage { get; set; }

    public Attribute ActionAttribute { get; set; }

    public ValidationResult Validate(PropertyInfo property, object validationObject)
    {
        ValidationResult validationResult = ValidationResult.SuccessfulResult;

        if (validationObject is Node node)
        {
            if (node.Owner == node)
            {
                return validationResult;
            }
        }
        
        
        object value = property.GetValue(validationObject);
        
        if (value == null)
        {
            validationResult = ValidationResult.FailedResult($"{property.Name} is not set");
        }

        return validationResult;
    }
}