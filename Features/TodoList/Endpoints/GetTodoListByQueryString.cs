namespace MinAPI.Demo.Features.TodoList.Endpoints;

public class GetTodoListByQueryString : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/GetByQueryString", HandleAsync)
        .WithOpenApi(op =>
        {
            op.OperationId = "GetTodoListByQueryString"; // 另一種指定作業識別碼的方式
            op.Summary = "多筆取得Todo"; // 摘要說明
            op.Description = "多筆取得Todo"; // 詳細說明
            return op;
        });
    }

    private record QueryTodoListRequest(Guid? TodoId, string? Name);

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
