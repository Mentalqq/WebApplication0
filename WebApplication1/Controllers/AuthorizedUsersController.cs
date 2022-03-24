using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Application.Commands;
using WebApplication1.DTO;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizedUsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthorizedUsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            UserDto deletedUser = await mediator.Send(new DeleteUser.Command { Id = id });
            return Ok(deletedUser);
        }
    }
}
