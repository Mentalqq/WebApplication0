using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Commands;
using WebApplication1.Data;
using static WebApplication1.Application.Commands.UserAddCommand;

namespace WebApplication1.AppCore.Tests.User.Commands
{
    [TestClass]
    public class UserAddCommandTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        private readonly UserAddCommandHandler commandHandler;
        public UserAddCommandTests()
        {
            commandHandler = new UserAddCommandHandler(userRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_OnSuccess_ShouldReturnTrue()
        {
            //Arrange
            var userCommand = new UserAddCommand
            {
                FirstName = "testFN",
                LastName = "testLN",
                Email = "testE",
                Age = 5
            };

            userRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Domain.User>()))
                .ReturnsAsync(true);

            //Act
            bool result = await commandHandler.Handle(userCommand, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Domain.User>()), Times.Once);
        }

        [TestMethod]
        public async Task Handle_OnFailed_ShouldReturnFalse()
        {
            //Arrange
            var userCommand = new UserAddCommand();

            userRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Domain.User>()))
                .ReturnsAsync(false);

            //Act
            bool result = await commandHandler.Handle(userCommand, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result);
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Domain.User>()), Times.Once);
        }
    }
}
