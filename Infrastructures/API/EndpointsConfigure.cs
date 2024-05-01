using MinAPI.Demo.Features.TodoList.Endpoints;
using MinAPI.Demo.Features.UserProfile.Endpoints;

namespace MinAPI.Demo.Infrastructures.API;
public static class EndpointsConfigure
{
    public static void MapEndpoint(this WebApplication app)
    {
        var endpoints = app.MapGroup("").WithOpenApi();

        endpoints.MapGroup("/todolist")
                 .WithTags("Todo 待辦清單")
                 .RequireAuthorization()
                 .MapEndpoint<GetTodoListByQueryString>()
                 .MapEndpoint<InsertTodoList>()
                 .MapEndpoint<InsertTodoLists>()
                 .MapEndpoint<UpdateIsComplete>();

        endpoints.MapGroup("/userprofile")
                .WithTags("使用者")
                .MapEndpoint<Login>();

    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
