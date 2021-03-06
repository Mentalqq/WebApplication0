using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Application.Options;
using WebApplication1.Data;
using WebApplication1.Infrastructure.Tests.Options;

namespace WebApplication1.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class UserRepositoryTests
    {
        private readonly UserRepository userRepository;
        private readonly MockIOptions<DapperConnectionOptions> connectionIOptions;

        public UserRepositoryTests()
        {
            connectionIOptions = new MockIOptions<DapperConnectionOptions>() 
            { 
                Value = new DapperConnectionOptions() 
            };
            userRepository = new UserRepository(connectionIOptions);
        }

        [TestMethod]
        public async Task WhenExists_ShouldReturnUserRepository()
        {
            //Arrange
            //Act
            //Assert
            Assert.IsNotNull(userRepository);
        }
    }
}
