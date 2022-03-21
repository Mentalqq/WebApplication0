using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Domain;

namespace WebApplication1.Application.Commands
{
    public class UpdateUser
    {
        public class Command : IRequest<User>
        {
            public User User { get; set; }
        }
        public class Handler : IRequestHandler<Command, User>
        {
            private IRepository _repository;
            public Handler(IRepository repository)
            {
                _repository = repository;
            }
            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.UpdateAsync(request.User);
            }
        }
    }
}
