using Dapper;
using Npgsql;
using System.Data;

namespace OutboxPattern.Pulling;

public static class DapperSingletonDatabase
{
    static DapperSingletonDatabase()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var connectionString = "Username=guven;Password=password;Server=localhost:5432;Database=postgres";
        _connection = new NpgsqlConnection(connectionString);
    }

    static IDbConnection _connection;

    public static IDbConnection Connection
    {
        get
        {
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            return _connection;
        }
    }

    public static async Task<IEnumerable<T>> QueryAsync<T>(string sql)
        => await _connection.QueryAsync<T>(sql);

    public static async Task<int> ExecuteAsync(string sql)
        => await _connection.ExecuteAsync(sql);

    static bool _dataReaderState = true;
    public static bool DataReaderState { get => _dataReaderState; }

    public static void DataReaderReady() => _dataReaderState = true;
    public static void DataReaderBusy() => _dataReaderState = false;
}
