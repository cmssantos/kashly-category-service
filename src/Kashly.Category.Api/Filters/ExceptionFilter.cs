using Cms.AspNetCore.JsonLocalizer.Interfaces;
using Kashly.Category.Communication.Responses;
using Kashly.Category.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kashly.Category.Api.Filters;

public class ExceptionFilter(ILocalizer localizer, ILogger<ExceptionFilter> logger) : IExceptionFilter
{
    private readonly ILocalizer localizer = localizer;
    private readonly ILogger<ExceptionFilter> logger = logger;

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is KashlyException)
        {
            HandleApplicationException(context);
        }
        else
        {
            ThrowUnhandledException(context);
        }
    }

    private static void HandleApplicationException(ExceptionContext context)
    {
        var cashControlException = context.Exception as KashlyException;
        var errorResponse = new ResponseErrorJson(cashControlException!.GetErrors());

        context.HttpContext.Response.StatusCode = cashControlException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnhandledException(ExceptionContext context)
    {
        // Log the unhandled exception
        this.logger.LogError(context.Exception, "Unhandled exception occurred. Path: {Path}, UserId: {UserId}",
            context.HttpContext.Request.Path, context.HttpContext.User?.Identity?.Name);

        var errorResponse = new ResponseErrorJson(this.localizer.GetString("error.serverError").Value);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
