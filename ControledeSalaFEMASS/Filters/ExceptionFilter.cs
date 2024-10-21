using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControledeSalaFEMASS.Filters;
public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is AppException exception)
        {
            HandleProjectException(exception, context);
        } else
        {
            ThrowUnkownException(context);
        }
    }
    private static void HandleProjectException(AppException exception, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)exception.GetStatusCode();
        context.Result = new ObjectResult(new ErrorDto(exception.GetErrorMessages()));
    }

    private static void ThrowUnkownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ErrorDto("Erro interno desconhecido"));
    }
}