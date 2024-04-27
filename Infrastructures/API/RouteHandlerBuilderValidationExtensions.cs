namespace MinAPI.Demo.Infrastructures.API;

public static class RouteHandlerBuilderValidationExtensions
{
    public static RouteHandlerBuilder WithRequestValidation<TRequest>(this RouteHandlerBuilder route) => route.AddEndpointFilter<RequestValidationFilter<TRequest>>().ProducesValidationProblem();
}
