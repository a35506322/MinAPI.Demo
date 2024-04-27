namespace MinAPI.Demo.Features.TodoList.Endpoints;

public class GetByQueryString : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint) => endpoint.MapGet("/GetByQueryString", HandleAsync);
    public record QueryTodoListRequest(string? Name);

    private static async Task<Ok<ResultResponse<List<Domain.Entities.TodoList>>>> HandleAsync(
        [AsParameters] QueryTodoListRequest request,
        TodoContext todoContext) =>
        TypedResults.Ok(ResponseFactory.CreateSuccessResponse(await todoContext.TodoList.ToListAsync()));
}
