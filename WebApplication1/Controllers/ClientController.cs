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
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetClients.Query());
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetClientById.Query { Id = id });
            if (result == null)
                return NotFound();
            return Ok(result);
            //return response == null ? NotFound() : Ok(response);

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(Client client)
        {
            var newItem = new Client
            {
                Name = client.Name,
                Age = client.Age,
            };

            var result = await _mediator.Send(new AddClient.Command { Client = client });
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(Client client)
        {
            var existClient = await _mediator.Send(new GetClientById.Query { Id = client.Id });
            if(existClient == null)
            {
                return BadRequest($"No client found with the id {client.Id}");
            }
            var _client = new Client()
            {
                Id = client.Id,
                Name = client.Name,
                Age = client.Age
            };
            var result = await _mediator.Send(new UpdateClient.Command { Client = _client });
            return Ok(result);
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _mediator.Send(new DeleteClient.Command { Id = id });
            return Ok(result);
        }
    }
}
