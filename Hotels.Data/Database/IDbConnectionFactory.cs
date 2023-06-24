using System.Data;
using System.Data.SqlClient;

namespace Hotels.Data.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync();
}

public class MsSqlConnection : IDbConnectionFactory
{
    private readonly string _connectionString;

    public MsSqlConnection(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateAsync()
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        return connection;
    }
}
