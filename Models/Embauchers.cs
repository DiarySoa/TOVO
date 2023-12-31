using Npgsql;
using Npgsql.Replication;

namespace TOVO.Models;

public class Embauchers
{
    public int id { get; set; }
    public int id_employe { get; set; }
    public DateTime date_embauche { get; set; }
    public string numero_cnaps { get; set; }
    public string nom { get; set; }
    public string prenom { get; set; }
    public DateTime date_de_naissance { get; set; }
    public string genre { get; set; }

    public Embauchers() { }

    public List<Embauchers> getAllEmbauchers()
    {
        List<Embauchers> serp = new List<Embauchers>();

        using NpgsqlConnection connection = Connect.GetSqlConnection();
        connection.Open();
        string query = "SELECT * FROM employe_embaucher";

        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Embauchers e = new Embauchers();
                    e.id = reader.GetInt32(0);
                    e. date_embauche = reader.GetDateTime(2);
                    e.numero_cnaps = reader.GetString(3);
                    e.nom = reader. GetString(4);
                    e.prenom = reader.GetString(5);
                    e.genre = reader.GetString(6);
                    e.date_de_naissance = reader.GetDateTime(7);
                    serp.Add(e);
                }
            }
        }
        connection.Close();

        return serp;
    }

   
}