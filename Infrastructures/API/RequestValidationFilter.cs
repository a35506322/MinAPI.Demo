
using MinAPI.Demo.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MinAPI.Demo.Infrastructures.API;

public class RequestValidationFilter<TRequest>(ILogger<RequestValidationFilter<TRequest>> logger, IValidator<TRequest>? validator = null) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        // 開頭變小寫
        ValidatorOptions.Global.PropertyNameResolver = (a, b, c) => b.Name.ToCamelCase();

        var requestName = typeof(TRequest).FullName;

        if (validator is null)
        {
            return await next(context);
        }

        var request = context.Arguments.OfType<TRequest>().First();
        var validationResult = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(ResponseFactory.VaildErrorResponse(validationResult.ToDictionary()));
        }

        return await next(context);
    }
}
