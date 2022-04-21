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
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public long Id { get; set; }
        public GetUserByIdQuery(long id)
        {
            if(id < 1)
                throw new ArgumentException("Id couldn't be less than 1");
            this.Id = id;
        }
        public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
        {
            private readonly IUserRepository repository;
            private readonly IMapper mapper;
            public GetUserByIdQueryHandler(IUserRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }
            public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                User user = await repository.GetByIdAsync(request.Id);
                return mapper.Map<User,UserDto>(user);
            }
        }
    }
}
