using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Domain;

namespace WebApplication1.Application.Queries
{
    public class GetUsers
    {
        public class Query : IRequest<List<User>>
        {
            public long Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, List<User>>
        {
            private IRepository _repository;
            public Handler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                var responce = await _repository.GetAllAsync();
                return responce.ToList();
            }
        }
    }
}
