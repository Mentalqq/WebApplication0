using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Domain;

namespace WebApplication1.Application.Queries
{
    public static class GetClientById
    {
        public class Query : IRequest<Client>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, Client>
        {
            private readonly IRepository _repository;
            public Handler(IRepository repository)
            {
                _repository = repository;
            }
            public async Task<Client> Handle(Query request, CancellationToken cancellationToken)
            {
                var responce = await _repository.GetByIdAsync(request.Id);
                return responce;
            }
        }
    }
}
