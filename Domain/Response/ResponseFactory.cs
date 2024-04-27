namespace MinAPI.Demo.Domain.Response;

public class ResponseFactory
{
    public static ResultResponse<T> CreateSuccessResponse<T>(T data) => new ResultResponse<T>(ReturnMessage: "查詢成功", ReturnData: data);

    public static ResultResponse InsertSuccessResponse() => new ResultResponse(ReturnMessage: "新增成功");
}
