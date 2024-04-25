namespace MinAPI.Demo.Domain.Response;

public static class ResponseFactory
{
    public static ResultResponse<T> CreateSuccessResponse<T>(T data) => new ResultResponse<T>(ReturnMessage: "查詢成功", ReturnData: data);
}
