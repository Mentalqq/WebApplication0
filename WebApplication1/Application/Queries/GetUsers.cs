using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;
using AutoMapper;
using WebApplication1.Domain;
using WebApplication1.DTO;
using WebApplication1.Application.Options;

namespace WebApplication1.Application.Queries
{
    public class GetUsers
    {
        public class Query : IRequest<List<UserDto>>
        {
            public long Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, List<UserDto>>
        {
            private readonly IUserRepository repository;
            private IMapper mapper;
            public Handler(IUserRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<List<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return mapper.Map<List<UserDto>>(await repository.GetAllAsync()).ToList();
            }
        }
    }
}
