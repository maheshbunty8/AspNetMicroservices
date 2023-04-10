using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

public class DbUtility<T> where T : DbConnection, new()
{
    private readonly string connectionString;

    public DbUtility(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<int> ExecuteNonQueryAsync(string query, CommandType commandType = CommandType.Text, IEnumerable<DbParameter> parameters = null)
    {
        using (var connection = new T())
        {
            connection.ConnectionString = connectionString;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<object> ExecuteScalarAsync(string query, CommandType commandType = CommandType.Text, IEnumerable<DbParameter> parameters = null)
    {
        using (var connection = new T())
        {
            connection.ConnectionString = connectionString;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                await connection.OpenAsync();
                return await command.ExecuteScalarAsync();
            }
        }
    }

    public async Task<IEnumerable<T>> ExecuteReaderAsync(string query, Func<IDataReader, T> mapper, CommandType commandType = CommandType.Text, IEnumerable<DbParameter> parameters = null)
    {
        var result = new List<T>();

        using (var connection = new T())
        {
            connection.ConnectionString = connectionString;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(mapper(reader));
                    }
                }
            }
        }

        return result;
    }
}