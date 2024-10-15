using System.Net;

namespace ControledeSalaFEMASS.Domain.Exceptions;
public abstract class AppException : SystemException
{
    protected AppException(string message) : base(message) { }

    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}