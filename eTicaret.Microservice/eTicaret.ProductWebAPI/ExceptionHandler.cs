using Microsoft.AspNetCore.Diagnostics;
using TS.Result;

namespace eTicaret.ProductWebAPI;

public sealed class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        Result<string> result = Result<string>.Failure(exception.Message);
        await httpContext.Response.WriteAsJsonAsync(result);

        return true;
    }
}
