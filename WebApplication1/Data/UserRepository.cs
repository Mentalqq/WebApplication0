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
        Task<bool> AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(long id);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(long id);
        Task<string> GetEmailAsync(string email);
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
                var sqlQuery = await db.QueryAsync<User>("select * from Users order by Id desc");
                return sqlQuery.ToList();
            }
        }

        public async Task<string> GetEmailAsync(string email)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = await db.QueryAsync<string>("select Email from Users where Email = @email", new {email});
                return sqlQuery.FirstOrDefault();
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
        public async Task<bool> AddAsync(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                user.CreatedDate = DateTime.UtcNow;
                user.ModifiedDate = DateTime.UtcNow;
                user.UserKey = Guid.NewGuid();
                var sqlQuery = @"insert into Users 
                    (UserKey, FirstName, LastName, Email, Age, CreatedDate, ModifiedDate) 
                    values (@UserKey, @FirstName, @LastName, @Email, @Age, @CreatedDate, @ModifiedDate)";
                await db.ExecuteAsync(sqlQuery, user);
            }
            return true;
        }
        public async Task<bool> UpdateAsync(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                user.ModifiedDate = DateTime.UtcNow;
                var sqlQuery = @"update Users set FirstName = @FirstName,
                    LastName = @LastName,
                    Email = @Email,
                    Age = @Age,
                    ModifiedDate = @ModifiedDate
                    where Id = @Id";
                await db.ExecuteAsync(sqlQuery, user);
            }
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "delete from Users where Id = @id";
                await db.ExecuteAsync(sqlQuery, new {id});
            }
            return true;
        }
    }
}
