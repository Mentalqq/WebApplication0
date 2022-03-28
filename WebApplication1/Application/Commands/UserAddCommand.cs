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
    public class UserAddCommand : IRequest<User>
    {
        public UserDto UserDto { get; set; }
        public sealed class UserAddCommandHandler : IRequestHandler<UserAddCommand, User>
        {
            private readonly IUserRepository repository;
            private IMapper mapper;
            public UserAddCommandHandler(IUserRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }
            public async Task<User> Handle(UserAddCommand request, CancellationToken cancellationToken)
            {
                return await repository.AddAsync(mapper.Map<User>(request.UserDto));
            }
        }
    }
}
