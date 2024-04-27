using FluentValidation.Results;
using MinAPI.Demo.Common.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Results;
using SharpGrip.FluentValidation.AutoValidation.Shared.Extensions;

namespace MinAPI.Demo.Infrastructures.FluentValidation;

public static class FluentValidationConfigure
{
    public static void FluentValidationSetting(this IServiceCollection services)
    {
        // 改小駝峰
        ValidatorOptions.Global.PropertyNameResolver = (a, b, c) => b.Name.ToCamelCase();
        // FluentValidation
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        // https://github.com/SharpGrip/FluentValidation.AutoValidation
        services.AddFluentValidationAutoValidation((configuration) =>
        {
            // Replace the default result factory with a custom implementation.
            configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });
    }
}

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IResult CreateResult(EndpointFilterInvocationContext context, ValidationResult validationResult)
    {
        return TypedResults.BadRequest(ResponseFactory.VaildErrorResponse(validationResult.ToValidationProblemErrors()));
    }
}
