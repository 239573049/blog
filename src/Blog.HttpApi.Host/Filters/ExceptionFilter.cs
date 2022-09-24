using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.HttpApi.Host.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        this._logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        var ex = context.Exception;

        if(ex is BusinessExceptions exceptions)
        {
            context.Result = new OkObjectResult(new HttpResult(exceptions.Code, exceptions.Message));

        }
        else
        {
            context.Result = new OkObjectResult(new HttpResult(500, ex.Message));
        }

        context.ExceptionHandled = true;
    }
}
