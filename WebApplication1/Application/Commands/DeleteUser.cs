﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Domain;

namespace WebApplication1.Application.Commands
{
    public class DeleteUser
    {
        public class Command : IRequest<User>
        {
            public long Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, User>
        {
            private IUserRepository repository;
            public Handler(IUserRepository repository)
            {
                this.repository = repository;
            }
            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                return await repository.DeleteAsync(request.Id);
            }
        }
    }
}
