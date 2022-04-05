using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Options;
using WebApplication1.Data;
using WebApplication1.Domain;
using WebApplication1.DTO;

namespace WebApplication1.Application.Queries
{
    public class UserGetByIdQuery : IRequest<UserDto>
    {
        public long Id { get; set; }
        public UserGetByIdQuery(long id)
        {
            this.Id = id;
        }
        public sealed class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, UserDto>
        {
            private readonly IUserRepository repository;
            private readonly IMapper mapper;
            public UserGetByIdQueryHandler(IUserRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }
            public async Task<UserDto> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
            {
                User user = await repository.GetByIdAsync(request.Id);
                return mapper.Map<UserDto>(user);
            }
        }
    }
}
