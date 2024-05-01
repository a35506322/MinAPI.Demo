namespace MinAPI.Demo.Features.UserProfile.Endpoints;

public class Login : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint) =>
        endpoint.MapPost("/login", HandleAsync)
                .WithOpenApi(op =>
                {
                    op.OperationId = "Login";
                    op.Summary = "登入";
                    return op;
                })
                .AddFluentValidationAutoValidation();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Account">帳號</param>
    /// <param name="Password">密碼</param>
    public record LoginRequest(string Account, string Password);
    public class RequestValidator : AbstractValidator<LoginRequest>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Account)
                .NotEmpty().WithMessage("帳號必輸");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("密碼必輸");
        }
    }

    private static async Task<Results<Ok<ResultResponse>, Ok<ResultResponse<string>>>> HandleAsync(
        [FromBody] LoginRequest request,
        TodoContext todoContext,
        JwtHelpers jwtHelpers)
    {
        var user = await todoContext.UserProfile.SingleOrDefaultAsync(x => x.Account == request.Account & x.Password == request.Password);

        if (user == null)
        {
            return TypedResults.Ok(ResponseFactory.LoginFailResponse());
        }
        return TypedResults.Ok(ResponseFactory.LoginSuccessResponse(jwtHelpers.GenerateToken(user.Account)));
    }
}

