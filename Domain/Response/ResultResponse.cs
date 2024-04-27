namespace MinAPI.Demo.Domain.Response;

public record ResultResponse<T>(ReturnCodeEnum ReturnCode = ReturnCodeEnum.Success, string ReturnMessage = "", T ReturnData = default);

public record ResultResponse(ReturnCodeEnum ReturnCode = ReturnCodeEnum.Success, string ReturnMessage = "", object ReturnData = null);
