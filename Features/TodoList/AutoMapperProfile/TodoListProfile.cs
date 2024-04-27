using static MinAPI.Demo.Features.TodoList.Endpoints.InsertTodoList;

namespace MinAPI.Demo.Features.TodoList.AutoMapperProfile;

public class TodoListProfile : Profile
{
    public TodoListProfile()
    {
        CreateMap<InsertTodoRequest, Domain.Entities.TodoList>();
    }
}