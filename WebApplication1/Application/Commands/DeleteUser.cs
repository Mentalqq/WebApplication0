using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Options;
using WebApplication1.Data;
using WebApplication1.Domain;
using WebApplication1.DTO;

namespace WebApplication1.Application.Commands
{
    public class DeleteUser
    {
        public class Command : IRequest<UserDto>
        {
            public long Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly IUserRepository repository;
            public Handler(IUserRepository repository)
            {
                this.repository = repository;
            }
            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                return MapperConfig.MapperUserToUserDto().Map<User, UserDto>(
                   await repository.DeleteAsync(request.Id));
            }
        }
    }
}
