using Microsoft.Extensions.Options;

namespace WebApplication1.Application.Options
{
    public class DapperConnectionOptions 
    {
        public string SqlServerConnection { get; set; }
    }
    public class ConnectionIOptions<T> : IOptions<T> where T : DapperConnectionOptions
    {
        public T Value { get; set; }
    }
}
