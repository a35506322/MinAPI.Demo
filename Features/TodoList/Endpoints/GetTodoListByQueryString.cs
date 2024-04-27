namespace MinAPI.Demo.Features.TodoList.Endpoints;

public class GetTodoListByQueryString : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint) => endpoint.MapGet("/GetByQueryString", HandleAsync);
    public record QueryTodoListRequest(Guid? TodoId, string? Name);

    private static async Task<Ok<ResultResponse<List<Domain.Entities.TodoList>>>> HandleAsync(
        [AsParameters] QueryTodoListRequest request,
        TodoContext todoContext)
    {
        var isParseTodoId = Guid.TryParse(request.TodoId.ToString(), out var todoId);
        var query = todoContext.TodoList
            .AsQueryable()
            .Where(x => String.IsNullOrEmpty(request.Name) || x.Name == request.Name)
            .Where(x => !isParseTodoId || x.TodoId == todoId);
        return TypedResults.Ok(ResponseFactory.CreateSuccessResponse(await query.ToListAsync()));
    }
}
