namespace MinAPI.Demo.Features.TodoList.Endpoints;

public class InsertTodoList : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint) =>
        endpoint.MapPost("/insert", HandleAsync)
                .WithOpenApi(op =>
                {
                    op.OperationId = "InsertTodoList";
                    op.Summary = "新增Todo"; // 摘要說明
                    return op;
                })
                .AddFluentValidationAutoValidation();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Name">姓名</param>
    /// <param name="Title">標題</param>
    /// <param name="TodoContent">內容</param>
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

    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<InsertTodoRequest, Domain.Entities.TodoList>();
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
