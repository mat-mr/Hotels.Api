using Dapper;
using Hotels.Data.Database;
using Hotels.Data.Models;

namespace Hotels.Data.Repositores;

public class HotelRepository : IHotelRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public HotelRepository(IDbConnectionFactory dbConnectionFactory) 
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<HotelDto?> GetByIdAsync(Guid id, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateAsync();

        var result = await connection.QuerySingleOrDefaultAsync(new CommandDefinition("""
                select * from hotels where id = @id
                """, new { id }, cancellationToken: token));

        if (result == null)
        {
            return null;
        }

        return MapHotel(result);
    }

    public async Task<HotelDto?> GetBySlugAsync(string slug, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateAsync();
        
        var result = await connection.QuerySingleOrDefaultAsync(new CommandDefinition("""
                select * from hotels where slug = @slug
                """, new { slug }, cancellationToken: token));

        if (result == null)
        {
            return null;
        }

        return MapHotel(result);
    }


    public async Task<HotelDto?> GetByNameAsync(string name, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateAsync();

        var result = await connection.QuerySingleOrDefaultAsync(new CommandDefinition("""
                select * from hotels where name = @name
                """, new { name }, cancellationToken: token));

        if (result == null)
        {
            return null;
        }

        return MapHotel(result);
    }

    public async Task<IEnumerable<HotelDto>> GetAllAsync(GetAllHotelsOptions options, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateAsync();

        var result = await connection.QueryAsync(new CommandDefinition($"""
                select * from hotels
                where (@name is null or name like @name)
                and (@category is null or category like @category)
                and (@includesTransfers is null or includesTransfers = @includesTransfers)
                """, 
                new { 
                    name = options.Name is null ? null : $"%{options.Name}%", 
                    category = options.Category is null ? null : $"%{options.Category}%", 
                    includesTransfers = options.IncludesTransfers is null ? null : options.IncludesTransfers},
                cancellationToken: token));

        return result.Select(MapHotel);
    }

    public async Task<bool> CreateAsync(HotelDto hotel, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateAsync();
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
                insert into hotels (id, name, category, includesTransfers)
                values (@Id, @Name, @Category, @IncludesTransfers)
                """, hotel, transaction, cancellationToken: token));

        transaction.Commit();

        return result > 0;
    }

    public async Task<bool> UpdateAsync(HotelDto hotel, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateAsync();
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
                update hotels
                set name=@Name, category=@Category, includesTransfers=@IncludesTransfers
                where id = @Id
                """, hotel, transaction, cancellationToken: token));

        transaction.Commit();

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateAsync();
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
                delete hotels where id = @id
                """, new {id}, transaction, cancellationToken: token));

        transaction.Commit();

        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateAsync();

        var result = await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
                select count(1) from hotels where id = @id
                """, new { id }, cancellationToken: token));

        return result;
    }

    private static HotelDto MapHotel(dynamic h)
    {
        return new HotelDto
        {
            Id = h.id,
            Name = h.name,
            Category = h.category,
            IncludesTransfers = h.includesTransfers,
        };
    }
}
