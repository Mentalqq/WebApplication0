using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Commands;
using WebApplication1.Data;
using static WebApplication1.Application.Commands.UserUpdateCommand;

namespace WebApplication1.AppCore.Tests.User.Commands
{
    [TestClass]
    public class UserUpdateCommandTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        private readonly UserUpdateCommandHandler commandHandler;
        public UserUpdateCommandTests()
        {
            commandHandler = new UserUpdateCommandHandler(userRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_OnSuccess_shouldReturnTrue()
        {
            //Arrange
            var userCommand = new UserUpdateCommand();

            userRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Domain.User>()))
                .ReturnsAsync(true);

            //Act
            bool result = await commandHandler.Handle(userCommand, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
            userRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.User>()), Times.Once);
        }

        [TestMethod]
        public async Task Handle_OnFailed_shouldReturnFalse()
        {
            //Arrange
            var userCommand = new UserUpdateCommand();

            userRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Domain.User>()))
                .ReturnsAsync(false);

            //Act
            bool result = await commandHandler.Handle(userCommand, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result);
            userRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.User>()), Times.Once);
        }
    }
}
