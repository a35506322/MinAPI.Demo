namespace MinAPI.Demo.Features.TodoList.Endpoints;

public class UpdateIsComplete : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint) =>
        endpoint.MapPut("/updateIsComplete/{todoId}", HandleAsync)
        .AddFluentValidationAutoValidation();

    public record UpdateIsCompleteRequest(Guid TodoId, string IsComplete);
    public class RequestValidator : AbstractValidator<UpdateIsCompleteRequest>
    {
        public RequestValidator()
        {
            RuleFor(x => x.TodoId)
                .NotEmpty().WithMessage("ID必輸");
            RuleFor(x => x.IsComplete)
                .NotEmpty().WithMessage("是否完成必輸")
                .Matches("Y|N").WithMessage("是否完成格式有誤");
        }
    }

    private static async Task<Ok<ResultResponse>> HandleAsync(
         [FromRoute] Guid todoId,
         [FromBody] UpdateIsCompleteRequest request,
        TodoContext todoContext)
    {
        if (!request.TodoId.Equals(todoId))
            return TypedResults.Ok(ResponseFactory.UpdateVaildErrorResponse(todoId.ToString()));

        var todoEntity = await todoContext.TodoList.SingleOrDefaultAsync(x => x.TodoId.Equals(request.TodoId));
        if (todoEntity is null)
            return TypedResults.Ok(ResponseFactory.UpdateVaildErrorResponse(todoId.ToString()));

        await todoContext.TodoList
            .Where(x => x.TodoId.Equals(request.TodoId))
            .ExecuteUpdateAsync(x => x.SetProperty(y => y.IsComplete, request.IsComplete));

        int count = await todoContext.SaveChangesAsync();

        return TypedResults.Ok(ResponseFactory.UpdateSuccessResponse());
    }
}

