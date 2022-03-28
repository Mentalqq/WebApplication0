﻿using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Options;
using WebApplication1.Data;
using WebApplication1.Domain;
using WebApplication1.DTO;

namespace WebApplication1.Application.Commands
{
    public class UserUpdateCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public UserDto UserDto { get; set; }
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
                return await repository.UpdateAsync(mapper.Map<User>(request.UserDto), request.Id);
            }
        }
    }
}
