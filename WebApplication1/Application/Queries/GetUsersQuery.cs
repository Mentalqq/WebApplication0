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
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
        public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
        {
            private readonly IUserRepository repository;
            private readonly IMapper mapper;
            public GetUsersQueryHandler(IUserRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<User> users = await repository.GetAllAsync();
                return mapper.Map<IEnumerable<User>, List<UserDto>>(users).ToList();
            }
        }
    }
}
