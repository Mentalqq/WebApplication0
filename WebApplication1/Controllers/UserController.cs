using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System;
using System.Threading.Tasks;
using WebApplication1.Application.Queries;
using WebApplication1.Domain;
using WebApplication1.Application.Commands;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await mediator.Send(new GetUsers.Query());
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await mediator.Send(new GetUserById.Query { Id = id });
            if (result == null)
                return NotFound();
            return Ok(result);
            //return response == null ? NotFound() : Ok(response);

        }

        [HttpPost("users")]
        public async Task<IActionResult> AddAsync(User user)
        {
            var result = await mediator.Send(new AddUser.Command { User = user });
            return Ok(result);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateAsync(User user)
        {
            var existUser = await mediator.Send(new GetUserById.Query { Id = user.Id });
            if(existUser == null)
            {
                return BadRequest($"No client found with the id {user.Id}");
            }
            var updatedUser = new User()
            {
                Id = existUser.Id, //Dont work without it, need create UserModel :D
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Age = user.Age,
                CreatedDate = user.CreatedDate,
            };
            var result = await mediator.Send(new UpdateUser.Command { User = updatedUser });
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await mediator.Send(new DeleteUser.Command { Id = id });
            return Ok(result);
        }
    }
}
