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
    public class UsersGetQuery : IRequest<List<UserDto>>
    {
        public sealed class UsersGetQueryHandler : IRequestHandler<UsersGetQuery, List<UserDto>>
        {
            private readonly IUserRepository repository;
            private readonly IMapper mapper;
            public UsersGetQueryHandler(IUserRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<List<UserDto>> Handle(UsersGetQuery request, CancellationToken cancellationToken)
            {
                return mapper.Map<List<UserDto>>(await repository.GetAllAsync()).ToList();
            }
        }
    }
}
