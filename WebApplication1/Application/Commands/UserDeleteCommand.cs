using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Application.Commands
{
    public class UserDeleteCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public UserDeleteCommand(long id)
        {
            this.Id = id;
        }
        public sealed class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, bool>
        {
            private readonly IUserRepository repository;
            public UserDeleteCommandHandler(IUserRepository repository)
            {
                this.repository = repository;
            }
            public async Task<bool> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
            {
                return await repository.DeleteAsync(request.Id);
            }
        }
    }
}
