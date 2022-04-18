using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.DTO;
using static WebApplication1.Application.Queries.GetUserByIdQuery;

namespace WebApplication1.AppCore.Tests.User.Queries
{
    [TestClass]
    public class GetUsersByIdQueryTests
    {
        private readonly Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
        private readonly Mock<IMapper> mapper = new Mock<IMapper>();
        private readonly GetUserByIdQueryHandler queryHandler;

        public GetUsersByIdQueryTests()
        {
            queryHandler = new GetUserByIdQueryHandler(userRepository.Object, mapper.Object);
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

            userRepository
                .Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

            mapper
                .Setup(x => x.Map<Domain.User, UserDto>(user))
                .Returns(userDto);

            //Act
            UserDto result = await queryHandler.Handle(new Application.Queries.GetUserByIdQuery(userId), CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
