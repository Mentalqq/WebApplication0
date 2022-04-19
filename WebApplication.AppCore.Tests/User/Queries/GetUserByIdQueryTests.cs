using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Queries;
using WebApplication1.Data;
using WebApplication1.DTO;
using static WebApplication1.Application.Queries.GetUserByIdQuery;

namespace WebApplication1.AppCore.Tests.User.Queries
{
    [TestClass]
    public class GetUserByIdQueryTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();
        private readonly GetUserByIdQueryHandler queryHandler;

        public GetUserByIdQueryTests()
        {
            queryHandler = new GetUserByIdQueryHandler(userRepositoryMock.Object, mapperMock.Object);
        }

        [TestMethod]
        public void Handle_WhenIdIsLessThanOne_ShouldThrowArgumentException()
        {
            //Arrange
            long id = 0;
            //Act
            //Assert
            Assert.ThrowsException<ArgumentException>(() => new GetUserByIdQuery(id));
        }

        [TestMethod]
        public async Task Handle_WhenExistsById_ShouldReturnUser()
        {
            //Arange
            long userId = 1;
            var user = new Domain.User();
            var userDto = new UserDto()
            {
                FirstName = "User1",
                LastName = "LUser1",
                Email = "User1@gmail.com"
            };

            userRepositoryMock
                .Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(user);

            mapperMock
                .Setup(x => x.Map<Domain.User, UserDto>(user))
                .Returns(userDto);

            //Act
            UserDto result = await queryHandler.Handle(new GetUserByIdQuery(userId), CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
