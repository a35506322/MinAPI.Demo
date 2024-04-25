namespace MinAPI.Demo.Domain.Response;

public record ResultResponse<T>(ReturnCodeEnum ReturnCode = ReturnCodeEnum.Success, string ReturnMessage = "", T ReturnData = default);
