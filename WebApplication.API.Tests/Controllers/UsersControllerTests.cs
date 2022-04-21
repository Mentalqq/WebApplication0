using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Commands;
using WebApplication1.Application.Queries;
using WebApplication1.Controllers;
using WebApplication1.DTO;
using WebApplication1.Infrastructure.Models;
using WebApplication1.ViewModel;

namespace WebApplication.API.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {
        private readonly Mock<IMediator> mediatorMock = new Mock<IMediator>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();
        private UsersController controller;

        public UsersControllerTests()
        {
            controller = new UsersController(mediatorMock.Object, mapperMock.Object);
        }

        [TestMethod]
        public async Task Get_WhenExists_ShouldReturnUsers()
        {
            //Arrange
            var usersDto = new List<UserDto>
            {
                new UserDto(),
                new UserDto(),
            };
            var userResponse = new[]
            {
                new UserGetResponse(),
                new UserGetResponse(),
            };

            mediatorMock
                .Setup(x => x.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(usersDto);

            mapperMock
                .Setup(x => x.Map<IEnumerable<UserDto>, UserGetResponse[]>(usersDto))
                .Returns(userResponse);

            //Act
            //ActionResult<CollectionResponse<UserGetResponse>>
            IActionResult response = await controller.GetAllAsync();

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.IsNotNull((response as OkObjectResult)?.Value);
        }

        [TestMethod]
        public async Task GetById_WhenExists_ShouldReturnUser()
        {
            //Arrange
            long id = 1;
            var userDto = new UserDto();
            var userResponse = new UserGetResponse();

            mediatorMock
                .Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userDto);

            mapperMock.Setup(x => x.Map<UserDto, UserGetResponse>(userDto))
                .Returns(userResponse);

            //Act
            IActionResult response = await controller.GetByIdAsync(id);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.IsNotNull((response as OkObjectResult)?.Value);
        }

        [TestMethod]
        public async Task Post_ShouldCallUserAddCommand()
        {
            //Arrange
            var userRequest = new UserAddRequest();

            mediatorMock
                .Setup(x => x.Send(It.IsAny<UserAddCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            IActionResult response = await controller.AddAsync(userRequest);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.IsNotNull((response as OkObjectResult)?.Value);
        }

        [TestMethod]
        public async Task Put_ShouldCallUserAddCommand()
        {
            //Arrange
            var userRequest = new UserUpdateRequest();

            mediatorMock
                .Setup(x => x.Send(It.IsAny<UserUpdateCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            IActionResult response = await controller.UpdateAsync(userRequest);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.IsNotNull((response as OkObjectResult)?.Value);
        }
    }
}
