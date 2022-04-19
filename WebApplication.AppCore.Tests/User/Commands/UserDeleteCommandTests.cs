using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Commands;
using WebApplication1.Data;
using static WebApplication1.Application.Commands.UserDeleteCommand;

namespace WebApplication1.AppCore.Tests.User.Commands
{
    [TestClass]
    public class UserDeleteCommandTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        private readonly UserDeleteCommandHandler commandHandler;

        public UserDeleteCommandTests()
        {
            commandHandler = new UserDeleteCommandHandler(userRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_OnSuccess_shouldReturnTrue()
        {
            //Arrange
            long id = 2;

            userRepositoryMock
                .Setup(x => x.DeleteAsync(id))
                .ReturnsAsync(true);

            //Act
            bool result = await commandHandler.Handle(new UserDeleteCommand(id), CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
            userRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [TestMethod]
        public async Task Handle_OnFailed_shouldReturnFalse()
        {
            //Arrange
            long id = -1;

            userRepositoryMock
                .Setup(x => x.DeleteAsync(id))
                .ReturnsAsync(false);

            //Act
            bool result = await commandHandler.Handle(new UserDeleteCommand(id), CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result);
            userRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }
    }
}
