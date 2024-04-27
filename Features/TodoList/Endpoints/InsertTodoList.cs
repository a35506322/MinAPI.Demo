namespace MinAPI.Demo.Features.TodoList.Endpoints;

public class InsertTodoList : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint) => endpoint.MapPost("/insert", HandleAsync);
    public record InsertTodoRequest(string Name, string Title, string TodoContent);
    private static async Task<Ok<ResultResponse>> HandleAsync(
        [FromBody] InsertTodoRequest request,
        TodoContext todoContext,
        IMapper mapper)
    {
        var todoEntity = mapper.Map<Domain.Entities.TodoList>(request);
        todoEntity.IsComplete = "N";
        todoEntity.AddTime = DateTime.Now;

        await todoContext.TodoList.AddAsync(todoEntity);
        var count = await todoContext.SaveChangesAsync();

        return TypedResults.Ok(ResponseFactory.InsertSuccessResponse());
    }
}
