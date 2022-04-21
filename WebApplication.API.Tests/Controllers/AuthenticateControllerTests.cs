using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Queries;
using WebApplication1.Controllers;
using WebApplication1.DTO;

namespace WebApplication.API.Tests.Controllers
{
    [TestClass]
    public class AuthenticateControllerTests
    {
        private readonly Mock<IMediator> mediatorMock = new Mock<IMediator>();
        private readonly AuthenticateController controller;

        public AuthenticateControllerTests()
        {
            controller = new AuthenticateController(mediatorMock.Object);
        }

        [TestMethod]
        public async Task Post_WhenExists_ShouldReturnToken()
        {
            //Arrange
            long id = 1;
            var user = new WebApplication1.Domain.User() { Id = id };
            var userDto = new UserDto() { Id = id, FirstName = "testFN" };

            mediatorMock
                .Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userDto);

            //Act
            IActionResult response = await controller.Login(user);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.IsNotNull((response as OkObjectResult)?.Value);
        }
    }
}
