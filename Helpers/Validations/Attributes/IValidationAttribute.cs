using System.Reflection;

namespace Valossy.Helpers.Validations.Attributes;

public interface IValidationAttribute
{
   public ValidationResult Validate(PropertyInfo property, object validationObject);
}
