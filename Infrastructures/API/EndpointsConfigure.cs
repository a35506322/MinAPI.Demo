using MinAPI.Demo.Features.TodoList.Endpoints;

namespace MinAPI.Demo.Infrastructures.API;
public static class EndpointsConfigure
{
    public static void MapEndpoint(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/todolist")
                .MapEndpoint<GetTodoListByQueryString>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
