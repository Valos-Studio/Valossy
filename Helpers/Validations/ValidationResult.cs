namespace Framework.Helpers.Validation;

public class ValidationResult
{
    public bool Success { get; set; }

    public string ValidationDescription { get; set; }
    
    public ValidationResult(bool success, string validationDescription)
    {
        Success = success;
        ValidationDescription = validationDescription;
    }

    public static ValidationResult SuccessfulResult { get; } = new ValidationResult(true, null);

    public static ValidationResult UnexpectedFailedResult { get; } = new ValidationResult(false, "Unexpected result");

    public static ValidationResult FailedResult(string errorMessage)
    {
        return new ValidationResult(false, errorMessage);
    }

    public override string ToString()
    {
        return $"Success: {Success}, ValidationDescription: {ValidationDescription}";
    }
}