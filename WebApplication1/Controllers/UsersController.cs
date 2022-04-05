using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System;
using System.Threading.Tasks;
using WebApplication1.Application.Queries;
using WebApplication1.Domain;
using WebApplication1.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.ViewModel;
using WebApplication1.DTO;
using WebApplication1.Application.Options;
using AutoMapper;
using System.Collections.Generic;
using WebApplication1.Infrastructure.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CollectionResponse<UserGetResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<UserDto> data = await mediator.Send(new UsersGetQuery());
            var mappedData = mapper.Map<IEnumerable<UserDto>, UserGetResponse[]>(data);

            var result = new CollectionResponse<UserGetResponse>
            {
                TotalCount = mappedData.Length,
                Data = mappedData
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SingleEntityResponse<UserGetResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            UserDto data = await mediator.Send(new UserGetByIdQuery(id));
            var mappedUser = mapper.Map<UserDto, UserGetResponse>(data);

            return Ok(new SingleEntityResponse<UserGetResponse>(mappedUser));

        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] UserAddRequest user)
        { 
            var result = await mediator.Send(new UserAddCommand
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Age = user.Age,
            });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateRequest user)
        {
            bool result = await mediator.Send(new UserUpdateCommand
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Age = user.Age,
            });
            return Ok(result);
        }
    }
}
