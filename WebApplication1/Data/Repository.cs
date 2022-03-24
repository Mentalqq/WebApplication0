using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Application.Options;
using WebApplication1.Domain;

namespace WebApplication1.Data
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(long id);
        Task<User> UpdateAsync(User user, long id);
        Task<User> DeleteAsync(long id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString = null;
        public UserRepository(IOptions<DapperConnectionOptions> options)
        {
            connectionString = options.Value.SqlServerConnection;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = await db.QueryAsync<User>("select * from Users");
                return sqlQuery.ToList();
            }
        }
        public async Task<User> GetByIdAsync(long id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = await db.QueryAsync<User>("select * from Users where Id = @id", new {id});
                return sqlQuery.FirstOrDefault();
            }
        }
        public async Task<User> AddAsync(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "insert into Users (UserKey,FirstName, LastName, Email, Age) values (@UserKey ,@FirstName, @LastName, @Email, @Age)";
                await db.ExecuteAsync(sqlQuery, user);
            }
            return user;
        }
        public async Task<User> UpdateAsync(User user, long id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "update Users set FirstName = @FirstName, LastName = @LastName, Email = @Email, Age = @Age, ModifiedDate = @ModifiedDate where Id = @Id";
                var parameters = new { Id = id, FirstName = user.FirstName, LastName = user.LastName,
                    Email = user.Email, Age = user.Age, ModifiedDate = user.ModifiedDate };
                await db.ExecuteAsync(sqlQuery, parameters);
            }
            return user;
        }
        public async Task<User> DeleteAsync(long id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "delete from Users where Id = @id";
                //await db.ExecuteAsync(sqlQuery, new {id});
            }

            var ttt = await GetByIdAsync(id);
            return ttt; // -_-
            //return await GetByIdAsync(id);
        }
    }
}
