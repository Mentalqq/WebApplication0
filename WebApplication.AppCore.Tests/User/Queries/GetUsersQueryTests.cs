using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Queries;
using WebApplication1.Data;
using WebApplication1.DTO;
using static WebApplication1.Application.Queries.GetUsersQuery;

namespace WebApplication1.AppCore.Tests.User.Queries
{
    [TestClass]
    public class GetUsersQueryTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();
        private readonly GetUsersQueryHandler queryHandler;

        public GetUsersQueryTests()
        {
            queryHandler = new GetUsersQueryHandler(userRepositoryMock.Object, mapperMock.Object);
        }

        [TestMethod]
        public async Task Handle_WhenExists_ShouldReturnUsers()
        {
            //Arange
            IEnumerable<Domain.User> users = new List<Domain.User>();
            List<UserDto> usersDto = new List<UserDto>
            {
                new UserDto {},
                new UserDto {},
                new UserDto {},
            };
            userRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(users);

            mapperMock
                .Setup(x => x.Map<IEnumerable<Domain.User>, IEnumerable<UserDto>>(users))
                .Returns(usersDto);

            //Act
            IEnumerable<UserDto> result = await queryHandler.Handle(new GetUsersQuery(), CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), usersDto.Count());
        }
    }
}
