using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

public class SqlBulkCopyAsync<T>
{
    private readonly string connectionString;
    private readonly string destinationTableName;

    public SqlBulkCopyAsync(string connectionString, string destinationTableName)
    {
        this.connectionString = connectionString;
        this.destinationTableName = destinationTableName;
    }

    public async Task BulkCopyAsync(IEnumerable<T> data)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.DestinationTableName = destinationTableName;
                        var table = new DataTable();

                        var properties = typeof(T).GetProperties();
                        foreach (var property in properties)
                        {
                            table.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                            bulkCopy.ColumnMappings.Add(property.Name, property.Name);
                        }

                        foreach (var item in data)
                        {
                            var row = table.NewRow();
                            foreach (var property in properties)
                            {
                                row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                            }
                            table.Rows.Add(row);
                        }

                        await bulkCopy.WriteToServerAsync(table);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}