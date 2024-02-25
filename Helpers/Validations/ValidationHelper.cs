using Framework.Helpers.Validation;
using System;
using Valossy.Loggers;

namespace HeavenAbandoned.Framework.Helpers.Validation;

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