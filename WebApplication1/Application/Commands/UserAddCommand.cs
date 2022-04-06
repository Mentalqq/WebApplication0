using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Options;
using WebApplication1.Data;
using WebApplication1.Domain;

namespace WebApplication1.Application.Commands
{
    public class UserAddCommand : IRequest<bool>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public sealed class UserAddCommandHandler : IRequestHandler<UserAddCommand, bool>
        {
            private readonly IUserRepository repository;
            public UserAddCommandHandler(IUserRepository repository)
            {
                this.repository = repository;
            }
            public async Task<bool> Handle(UserAddCommand request, CancellationToken cancellationToken)
            {
                User user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Age = request.Age,
                };
                return await repository.AddAsync(user);
            }
        }
    }
}
