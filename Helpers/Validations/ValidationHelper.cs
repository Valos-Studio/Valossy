using System;
using Valossy.Loggers;

namespace Valossy.Helpers.Validations;

public partial class ValidationHelper
{
    public static string[] GetConfigurationWarnings(object validationObject)
    {
        var result = ValidationBuilder.ValidateAll(validationObject);

        if (result.Success() == false)
        {
            string[] errorResults = result.GetResultsAsErrorArray();
            Logger.Error(string.Join("\n", errorResults));

            return errorResults;
        }

        return Array.Empty<string>();
    }
}