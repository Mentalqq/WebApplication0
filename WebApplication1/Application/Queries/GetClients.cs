using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Domain;

namespace WebApplication1.Application.Queries
{
    public class GetClients
    {
        public class Query : IRequest<List<Client>>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, List<Client>>
        {
            private IRepository _repository;
            public Handler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Client>> Handle(Query request, CancellationToken cancellationToken)
            {
                var responce = await _repository.GetAllAsync();
                return responce.ToList();
            }
        }
    }
}
