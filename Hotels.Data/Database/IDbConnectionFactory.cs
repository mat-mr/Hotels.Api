using Npgsql;
using System.Data;
using System.Data.SqlClient;

namespace Hotels.Data.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync();
}

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        return connection;
    }
}
