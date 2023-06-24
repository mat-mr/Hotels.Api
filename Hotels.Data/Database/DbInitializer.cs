using Dapper;

namespace Hotels.Data.Database;

public class DbInitializer
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DbInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _dbConnectionFactory.CreateAsync();

        await connection.ExecuteAsync("""
                if not exists (select * from sysobjects where name='hotels' and xtype='U') create table hotels (
                id uniqueidentifier primary key,
                name TEXT not null,
                category TEXT not null,
                includesTransfers BIT not null);
                """);
    }
}
