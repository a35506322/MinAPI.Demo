namespace MinAPI.Demo.Features.TodoList.Endpoints;

public class GetByQueryString : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint) => endpoint.MapGet("/GetByQueryString", HandleAsync);
    public record QueryTodoListRequest(string? Name);

    private static async Task<Ok<ResultResponse<List<TodoListEntity>>>> HandleAsync([AsParameters] QueryTodoListRequest request)
    {
        Console.WriteLine(request.Name);
        TodoListEntity todoListEntity = new TodoListEntity();
        List<TodoListEntity> todoListEntities = [todoListEntity];
        return TypedResults.Ok(ResponseFactory.CreateSuccessResponse(todoListEntities));
    }
}
