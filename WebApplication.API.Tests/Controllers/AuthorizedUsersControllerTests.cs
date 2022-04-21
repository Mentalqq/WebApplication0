using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WebApplication1.Application.Commands;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.API.Tests.Controllers
{
    [TestClass]
    public class AuthorizedUsersControllerTests
    {
        private readonly Mock<IMediator> mediatorMock = new Mock<IMediator>();
        private AuthorizedUsersController controller;

        public AuthorizedUsersControllerTests()
        {
            controller = new AuthorizedUsersController(mediatorMock.Object);
        }

        [TestMethod]
        public async Task DeleteById_WhenExists_ShouldReturnTrue()
        {
            //Arrange
            long id = 1;

            mediatorMock
                .Setup(x => x.Send(new UserDeleteCommand(id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            IActionResult response = await controller.DeleteAsync(id);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.IsNotNull((response as OkObjectResult)?.Value);
        }
    }
}
