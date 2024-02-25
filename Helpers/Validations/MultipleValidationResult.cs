using System.Collections.Generic;
using System.Linq;
using Framework.Helpers.Validation;

namespace HeavenAbandoned.Framework.Helpers.Validation
{
    public class MultipleValidationResult
    {
		private readonly List<ValidationResult> validationResults = new List<ValidationResult>();
        
		public void AddValidationResult(ValidationResult validationResult)
		{
			this.validationResults.Add(validationResult);
		}

		public string[] GetResultsAsErrorArray()
		{
			return validationResults.Where(x => x.Success == false).Select(x => x.ValidationDescription).ToArray();
		}

		public bool Success()
		{
			return validationResults.Any(x => x.Success == false) == false;
		}
    }
}