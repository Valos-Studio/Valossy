namespace Valossy.Helpers.Validations;

public interface IValidate<T>
{
	public ValidationResult Validate(T entity);
}
