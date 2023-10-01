using Npgsql;
public class Connect
{
    public static NpgsqlConnection GetSqlConnection()
    {
        string connectionString = "Host=localhost;Database=rh;Username=postgres;Password=DIARISOA";
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        return connection;
    }
}