using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace UserManagement.Api.ExceptionHandler;

public static class WebApplicationExtensions
{
    public static void HandleException(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(
                        new ErrorDetails(context.Response.StatusCode, "Internal Server Error."
                        ).ToString());
                }
            });
        });
    }
}
