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
    public static class GetUserById
    {
        public class Query : IRequest<UserDto>
        {
            public long Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly IUserRepository repository;
            private IMapper mapper;
            public Handler(IUserRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }
            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                return mapper.Map<UserDto>(await repository.GetByIdAsync(request.Id));
            }
        }
    }
}
