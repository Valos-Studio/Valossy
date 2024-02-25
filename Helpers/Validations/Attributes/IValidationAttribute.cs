using System.Reflection;

namespace Framework.Helpers.Validation.Attributes;


public interface IValidationAttribute
{
   public ValidationResult Validate(PropertyInfo property, object validationObject);
}
