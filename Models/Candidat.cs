using Npgsql;

namespace TOVO.Models;

public class Candidat
{
    public int id { get; set; }
    public string nom { get; set; }
    public string prenom { get; set; }
    public DateTime date_de_naissance{ get; set; }
    public string email { get; set; }
    public string sexe { get; set; }
    public string telephone { get; set; }


    public Candidat(){}

    public void Insert_Candidat(String nom, String prenom, DateTime dtn, String email, String sexe, String tel)
    {

        using (NpgsqlConnection connection = Connect.GetSqlConnection())
        {
            connection.Open();

            string query = "INSERT INTO candidat VALUES( default ,'" + nom + "','" + prenom + "','" + dtn + "','" + email + "','"+ sexe +"','"+ tel +"')";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public int id_last_register()
    {
        int id = 0;

        using NpgsqlConnection connection = Connect.GetSqlConnection();
        connection.Open();
        string query = "SELECT * FROM candidat ORDER BY id DESC limit 1";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            }
        }
        connection.Close();
        return id;
    }

    public void Insert_Candidat_diplome(String niveau, String diplome, String cv, String lm)
    {
       int id_c = id_last_register();

        using (NpgsqlConnection connection = Connect.GetSqlConnection())
        {
            connection.Open();

            string query = "INSERT INTO candidat_diplome VALUES( default ," + id_c + ",'" + niveau + "','" + diplome + "','" + cv + "','" + lm + "')";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}