using Npgsql;
using Npgsql.Replication;

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
    public string situation_patrimoniale { get; set; }
    public string adresse { get; set; }
    public string region { get; set; }
    public string province { get; set; }


    public Candidat(){}

    public void Insert_Candidat(String nom, String prenom, DateTime dtn, String email, String sexe, String tel, String situation, String adresse,String region, String province)
    {

        using (NpgsqlConnection connection = Connect.GetSqlConnection())
        {
            connection.Open();

            string query = "INSERT INTO candidat VALUES( default ,'" + nom + "','" + prenom + "','" + dtn + "','" + email + "','"+ sexe +"','"+ tel + "','"+situation+"','"+adresse+"','"+region+"','"+province+"')";

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

    public void Insert_Candidat_diplome(String niveau, String diplome, String cv, String lm, int experience)
    {
       int id_c = id_last_register();

        using (NpgsqlConnection connection = Connect.GetSqlConnection())
        {
            connection.Open();

            string query = "INSERT INTO candidat_diplome VALUES( default ," + id_c + ",'" + niveau + "','" + diplome + "','" + cv + "','" + lm + "'," + experience + ")";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public List<DateTime> GetAllDtn()
    {
        List<DateTime> dates = new List<DateTime>();

        using NpgsqlConnection connection = Connect.GetSqlConnection();
        connection.Open();
        string query = "SELECT dtn FROM candidat";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dates.Add(reader.GetDateTime(0));
                }
            }
        }
        connection.Close();
        return dates;
    }

    public List<int> CalculateAges(List<DateTime> datesDeNaissance)
    {
        datesDeNaissance = GetAllDtn();
        List<int> ages = new List<int>();
        DateTime currentDate = DateTime.Now;

        foreach (var dateDeNaissance in datesDeNaissance)
        {
            int age = currentDate.Year - dateDeNaissance.Year;

            // Vérifiez si l'anniversaire de la personne est déjà passé cette année
            if (currentDate.Month < dateDeNaissance.Month || (currentDate.Month == dateDeNaissance.Month && currentDate.Day < dateDeNaissance.Day))
            {
                age--; 
            }

            ages.Add(age);
        }

        return ages;
    }
}