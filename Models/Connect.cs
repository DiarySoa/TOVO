using Npgsql;

public class Connect
{
    public static NpgsqlConnection GetSqlConnection()
    {
        string connectionString =
            "Host=localhost;Database=ressource;Username=postgres;Password=root";
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        return connection;
    }
}
