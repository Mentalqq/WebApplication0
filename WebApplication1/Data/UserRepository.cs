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
        Task<bool> UpdateAsync(User user, long id);
        Task<bool> DeleteAsync(long id);
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
                user.CreatedDate = DateTime.UtcNow;
                var sqlQuery = "insert into Users (UserKey,FirstName, LastName, Email, Age, CreatedDate) " +
                    "values (@UserKey ,@FirstName, @LastName, @Email, @Age, @CreatedDate)";
                await db.ExecuteAsync(sqlQuery, user);
            }
            return user;
        }
        public async Task<bool> UpdateAsync(User user, long id)
        {
            if (GetByIdAsync(id) is null)
                return false;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "update Users set FirstName = @FN, LastName = @LN, Email = @E, " +
                    "Age = @A, ModifiedDate = @MD where Id = @Id";
                var parameters = new { Id = id, FN = user.FirstName, LN = user.LastName,
                    E = user.Email, A = user.Age, MD = DateTime.UtcNow };
                await db.ExecuteAsync(sqlQuery, parameters);
            }
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            if (GetByIdAsync(id) is null)
                return false;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "delete from Users where Id = @id";
                await db.ExecuteAsync(sqlQuery, new {id});
            }
            return true;
        }
    }
}
