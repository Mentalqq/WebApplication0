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
    public interface IRepository
    {
        Task<Client> AddAsync(Client client);
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(int id);
        Task<Client> UpdateAsync(Client client);
        Task<Client> DeleteAsync(int id);
    }
    public class Repository : IRepository
    {
        string connectionString = null;
        public Repository(IOptions<ConnectionStringOptions> options)
        {
            connectionString = options.Value.connectionString;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = await db.QueryAsync<Client>("select * from Users");
                return sqlQuery.ToList();
            }
        }
        public async Task<Client> GetByIdAsync(int id)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = await db.QueryAsync<Client>("select * from Users where Id = @id", new {id});
                return sqlQuery.FirstOrDefault();
            }
        }
        public async Task<Client> AddAsync(Client client)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "insert into Users (Name, Age) values (@Name, @Age)";
                await db.ExecuteAsync(sqlQuery, client);
            }
            return client;
        }
        public async Task<Client> UpdateAsync(Client client)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "update Users set Name = @Name, Age = @Age where Id = @id";
                await db.ExecuteAsync(sqlQuery, client);
            }
            return client;
        }
        public async Task<Client> DeleteAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "delete from Users where Id = @id";
                await db.ExecuteAsync(sqlQuery, new {id});
            }
            return await GetByIdAsync(id);
        }
    }
}
