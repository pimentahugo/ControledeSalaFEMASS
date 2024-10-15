using System.Net;

namespace ControledeSalaFEMASS.Domain.Exceptions;
public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}