namespace MinAPI.Demo.Domain.Response;

public class ResponseFactory
{
    public static ResultResponse<T> CreateSuccessResponse<T>(T data) => new ResultResponse<T>(ReturnMessage: "查詢成功", ReturnData: data);

    public static ResultResponse InsertSuccessResponse() => new ResultResponse(ReturnMessage: "新增成功");

    public static ResultResponse UpdateSuccessResponse() => new ResultResponse(ReturnMessage: "修改成功");

    public static ResultResponse UpdateVaildErrorResponse(string id) => new ResultResponse(ReturnMessage: $"驗證失敗，此{id}有誤");

    public static ResultResponse<IDictionary<string, string[]>> VaildErrorResponse(IDictionary<string, string[]> errors) => new ResultResponse<IDictionary<string, string[]>>(ReturnCode: ReturnCodeEnum.VaildDataError, ReturnMessage: "驗證失敗", ReturnData: errors);

    public static ResultResponse<ProblemDetails> ServerErrorResponse(ProblemDetails errors) => new ResultResponse<ProblemDetails>(ReturnCode: ReturnCodeEnum.ServerError, ReturnMessage: "意外狀況", ReturnData: errors);
}
