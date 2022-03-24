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
            public Handler(IUserRepository repository)
            {
                this.repository = repository;
            }

            public async Task<List<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return MapperConfig.MapperUserToUserDto().Map<IEnumerable<User>, IEnumerable<UserDto>>(
                    await repository.GetAllAsync()).ToList();
            }
        }
    }
}
