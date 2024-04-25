namespace MinAPI.Demo.Infrastructures.API;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder endpoint);
}
