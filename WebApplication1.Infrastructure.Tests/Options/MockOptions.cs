using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Application.Options;

namespace WebApplication1.Infrastructure.Tests.Options
{
    public class MockIOptions<T> : IOptions<T> where T : DapperConnectionOptions
    {
        public T Value { get; set; }
    }
}
