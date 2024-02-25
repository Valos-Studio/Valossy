using System;
using System.Collections.Generic;
using System.Reflection;
using Framework.Helpers.Validation.Attributes;
using HeavenAbandoned.Framework.Helpers.Validation;

namespace Framework.Helpers.Validation;

public class ValidationBuilder 
{
    private readonly List<Func<ValidationResult>> validationActions = new List<Func<ValidationResult>>();

    public static ValidationResult Validate(object validationObject)
    {
        ValidationBuilder builder = ProcessValidations(validationObject);

        return builder.Validate();
    }

    public static MultipleValidationResult ValidateAll(object validationObject)
    {
         ValidationBuilder builder = ProcessValidations(validationObject);

        return builder.ValidateAll();
    }

    public ValidationBuilder AddValidation(Func<ValidationResult> validationAction)
    {
        validationActions.Add(validationAction);
        return this;
    }

    public ValidationBuilder AddNullPropertyValidation(object value, string propertyName)
    {
        validationActions.Add(() => value == null ? new ValidationResult(false, $"{propertyName} is not set") : ValidationResult.SuccessfulResult);
        return this;
    }

    public ValidationResult Validate()
    {
        ValidationResult overallValidationResult = ValidationResult.SuccessfulResult;

        foreach(var validationAction in validationActions)
        {
            ValidationResult validationResult = validationAction.Invoke();

            if(validationResult == null)
            {
                overallValidationResult = ValidationResult.UnexpectedFailedResult;
                break;
            }

            if(validationResult.Success == false)
            {
                overallValidationResult = validationResult;
                break;
            }
        }

        return overallValidationResult;
    }

    public MultipleValidationResult ValidateAll()
    {
        MultipleValidationResult overallValidationResult = new MultipleValidationResult();

        foreach(var validationAction in validationActions)
        {
            ValidationResult validationResult = validationAction.Invoke();

            if(validationResult == null)
            {
                overallValidationResult.AddValidationResult(ValidationResult.UnexpectedFailedResult);
                break;
            }

            if(validationResult.Success == false)
            {
                overallValidationResult.AddValidationResult(validationResult);
                break;
            }
        }

        return overallValidationResult;
    }

    private static ValidationBuilder ProcessValidations(object validationObject)
    {
        var type = validationObject.GetType();

        var properties = type.GetProperties();
        ValidationBuilder builder = new ValidationBuilder();
        
        if(properties != null)
        {
            foreach(PropertyInfo property in properties)
            {
                var notNullAttribute = property.GetCustomAttribute<NotNullValidation>();
                if(notNullAttribute != null)
                {
                    builder.AddValidation(() => notNullAttribute.Validate(property, validationObject));
                }
            }
        }

        return builder;
    }
}