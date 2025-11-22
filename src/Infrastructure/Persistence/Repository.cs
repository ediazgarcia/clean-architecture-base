using Application.Interfaces;
using Dapper;
using Domain.Common;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Persistence;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IDbConnection _connection;
    private readonly string _tableName;

    public Repository(IDbConnection connection)
    {
        _connection = connection;
        _tableName = typeof(T).Name;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var sql = $"SELECT * FROM [{_tableName}] WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public async Task<List<T>> GetAllAsync()
    {
        var sql = $"SELECT * FROM [{_tableName}]";
        var result = await _connection.QueryAsync<T>(sql);
        return result.ToList();
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.Id = Guid.NewGuid();
        
        var properties = typeof(T).GetProperties()
            .Where(p => p.Name != "Id")
            .Select(p => p.Name);
        
        var columns = string.Join(", ", properties.Select(p => $"[{p}]"));
        var values = string.Join(", ", properties.Select(p => $"@{p}"));
        
        var sql = $@"
            INSERT INTO [{_tableName}] (Id, {columns})
            VALUES (@Id, {values})";
        
        await _connection.ExecuteAsync(sql, entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        var properties = typeof(T).GetProperties()
            .Where(p => p.Name != "Id")
            .Select(p => p.Name);
        
        var setClause = string.Join(", ", properties.Select(p => $"[{p}] = @{p}"));
        
        var sql = $@"
            UPDATE [{_tableName}]
            SET {setClause}
            WHERE Id = @Id";
        
        await _connection.ExecuteAsync(sql, entity);
    }

    public async Task DeleteAsync(T entity)
    {
        var sql = $"DELETE FROM [{_tableName}] WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, new { entity.Id });
    }
}
