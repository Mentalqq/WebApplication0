using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class EvolveFactory
    {
        public static Evolve.Evolve Create(SqlConnection cnx, string location)
        {
            return new Evolve.Evolve(cnx)
            {
                Locations = new[] { location },
                IsEraseDisabled = true,
                OutOfOrder = true,
                MetadataTableName = "MigrationHistory"
            };
        }
    }
}
