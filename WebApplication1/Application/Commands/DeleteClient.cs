using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Domain;

namespace WebApplication1.Application.Commands
{
    public class DeleteClient
    {
        public class Command : IRequest<Client>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, Client>
        {
            private IRepository _repository;
            public Handler(IRepository repository)
            {
                _repository = repository;
            }
            public async Task<Client> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.DeleteAsync(request.Id);
            }
        }
    }
}
