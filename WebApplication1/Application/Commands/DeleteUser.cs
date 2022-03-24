using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Options;
using WebApplication1.Data;
using WebApplication1.Domain;
using WebApplication1.DTO;

namespace WebApplication1.Application.Commands
{
    public class DeleteUser
    {
        public class Command : IRequest<UserDto>
        {
            public long Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly IUserRepository repository;
            private IMapper mapper;
            public Handler(IUserRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }
            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                return mapper.Map<UserDto>(await repository.DeleteAsync(request.Id));
            }
        }
    }
}
