namespace Framework.Helpers.Validation;

public interface IValidate<T>
{
	public ValidationResult Validate(T entity);
}
