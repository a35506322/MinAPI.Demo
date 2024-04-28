namespace MinAPI.Demo.Features.TodoList.Endpoints;

public class InsertTodoLists : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint) =>
        endpoint.MapPost("/insertTodoList", HandleAsync)
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

    public class RequestsValidator : AbstractValidator<List<InsertTodoRequest>>
    {
        public RequestsValidator()
        {
            RuleForEach(source => source).SetValidator(new RequestValidator());
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
        [FromBody] List<InsertTodoRequest> request,
        TodoContext todoContext,
        IMapper mapper)
    {
        var todoEntities = mapper.Map<List<Domain.Entities.TodoList>>(request);

        todoEntities.ForEach(item =>
        {
            item.IsComplete = "N";
            item.AddTime = DateTime.Now;
        });

        await todoContext.TodoList.AddRangeAsync(todoEntities);
        var count = await todoContext.SaveChangesAsync();

        return TypedResults.Ok(ResponseFactory.InsertSuccessResponse());
    }
}
