using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Application.Options;
using WebApplication1.Data;

namespace WebApplication1.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class UserRepositoryTests
    {
        private readonly UserRepository userRepository;
        private readonly ConnectionIOptions<DapperConnectionOptions> connectionIOptions;

        public UserRepositoryTests()
        {
            connectionIOptions = new ConnectionIOptions<DapperConnectionOptions>() 
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
