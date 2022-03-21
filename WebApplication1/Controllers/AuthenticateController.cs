using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Application.Queries;
using WebApplication1.Domain;
using WebApplication1.Application.Options;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;
        //private readonly IConfiguration _configuration;

        public AuthenticateController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            //_configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            var existUser = await _mediator.Send(new GetUserById.Query { Id = model.Id });
            var identity = await GetIdentity(model);
            if (existUser != null)
            {
                //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT: SecretKey"]));
                var token = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,//_configuration["JWT: ValidIssuer"],
                    audience: AuthOptions.AUDIENCE,//_configuration["JWT: ValidAudience"],
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        private async Task<ClaimsIdentity> GetIdentity(User model)
        {
            User _user = await _mediator.Send(new GetUserById.Query { Id = model.Id });
            if (_user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, _user.FirstName)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}