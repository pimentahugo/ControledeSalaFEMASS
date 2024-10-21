using System.Net;

namespace ControledeSalaFEMASS.Domain.Exceptions;
public class OperationInvalidException : AppException
{
    public OperationInvalidException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}