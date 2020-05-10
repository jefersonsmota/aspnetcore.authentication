namespace authentication.application.Handlers
{
    public abstract class BaseValidationHandler
    {
        public bool IsValid { get; protected set; }
        public string ErrorMessage { get; protected set; }
    }

    public interface IBaseValidator
    {
        bool IsValid();
        string ErrorMessage();
    }
}
