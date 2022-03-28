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
        public async Task<IActionResult> GetAllAsync()
        {
            var result = new UserGetResponse { Users = await mediator.Send(new UsersGetQuery()) };
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = new UserGetByIdResponse { User = await mediator.Send(new UserGetByIdQuery(id)) };
            if (result == null)
                return NotFound();
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] UserAddRequest user)
        {
            var result = await mediator.Send(new UserAddCommand { 
                UserDto = mapper.Map<UserDto>(user) });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateRequest user, long id)
        {
            var existUser = new UserGetByIdResponse { User = await mediator.Send(new UserGetByIdQuery(id)) };
            if (existUser.User == null)
            {
                return BadRequest($"No client found with the id {id}");
            }
            bool result = await mediator.Send(new UserUpdateCommand { 
                Id = id, UserDto = mapper.Map<UserDto>(user) });
            return Ok(result);
        }
        
    }
}
