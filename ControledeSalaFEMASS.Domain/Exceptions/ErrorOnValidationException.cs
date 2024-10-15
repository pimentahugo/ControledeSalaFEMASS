using System.Net;

namespace ControledeSalaFEMASS.Domain.Exceptions;
public class ErrorOnValidationException : AppException
{
    private readonly IList<string> _errorMessages;

    public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
    {
        _errorMessages = errorMessages;
    }

    public ErrorOnValidationException(string errorMessage) : base(string.Empty)
    {
        _errorMessages = new List<string> { errorMessage };
    }

    public override IList<string> GetErrorMessages() => _errorMessages;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}