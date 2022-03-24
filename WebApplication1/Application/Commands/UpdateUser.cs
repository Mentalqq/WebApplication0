using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Options;
using WebApplication1.Data;
using WebApplication1.Domain;
using WebApplication1.DTO;

namespace WebApplication1.Application.Commands
{
    public class UpdateUser
    {
        public class Command : IRequest<User>
        {
            public long Id { get; set; }
            public UserDto UserDto { get; set; }
        }
        public class Handler : IRequestHandler<Command, User>
        {
            private readonly IUserRepository repository;
            public Handler(IUserRepository repository)
            {
                this.repository = repository;
            }
            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = MapperConfig.MapperUserDtoToUser().Map<UserDto, User>(request.UserDto);
                return await repository.UpdateAsync(user, request.Id);
            }
        }
    }
}
