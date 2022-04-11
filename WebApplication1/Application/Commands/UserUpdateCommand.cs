using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Options;
using WebApplication1.Data;
using WebApplication1.Domain;
using WebApplication1.DTO;
using WebApplication1.Validation;

namespace WebApplication1.Application.Commands
{
    public class UserUpdateCommand : IRequest<bool>, IUserValidationModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public sealed class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, bool>
        {
            private readonly IUserRepository repository;
            private readonly IMapper mapper;
            public UserUpdateCommandHandler(IUserRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }
            public async Task<bool> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
            {
                User user = new User
                {
                    Id = request.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Age = request.Age,
                };
                return await repository.UpdateAsync(user);
            }
        }
    }
}
