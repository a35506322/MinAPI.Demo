namespace MinAPI.Demo.Features.TodoList.Endpoints;

public class InsertTodoList : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint) =>
        endpoint.MapPost("/insert", HandleAsync)
        .AddFluentValidationAutoValidation();

    public record InsertTodoRequest(string Name, string Title, string TodoContent);
    public class RequestValidator : AbstractValidator<InsertTodoRequest>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("姓名必輸");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("標題必輸");
            RuleFor(x => x.TodoContent)
               .NotEmpty().WithMessage("完成事項內容必輸");
        }
    }

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
