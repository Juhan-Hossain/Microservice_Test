using global::UserService.Models;
using MediatR;

namespace UserService.Queries;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public string Id { get; }

    public GetUserByIdQuery(string id)
    {
        Id = id;
    }
}
