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
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetUsers.Query());
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _mediator.Send(new GetUserById.Query { Id = id });
            if (result == null)
                return NotFound();
            return Ok(result);
            //return response == null ? NotFound() : Ok(response);

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(User user)
        {
            var result = await _mediator.Send(new AddUser.Command { User = user });
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(User user)
        {
            var existUser = await _mediator.Send(new GetUserById.Query { Id = user.Id });
            if(existUser == null)
            {
                return BadRequest($"No client found with the id {user.Id}");
            }
            var _user = new User()
            {
                Id = existUser.Id, //Dont work without it, need create UserModel :D
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Age = user.Age,
                CreatedDate = user.CreatedDate,
            };
            var result = await _mediator.Send(new UpdateUser.Command { User = _user });
            return Ok(result);
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _mediator.Send(new DeleteUser.Command { Id = id });
            return Ok(result);
        }
    }
}
