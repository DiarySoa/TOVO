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
    public Service_Poste service_poste { get; set; }



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

    public int lastId()
    {
        int s = 0;

        using NpgsqlConnection connection = Connect.GetSqlConnection();
        connection.Open();
        string query = "SELECT * FROM candidat_diplome ORDER BY id DESC limit 1";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    s = reader.GetInt32(0);
                }
            }
        }
        connection.Close();

        return s;
    }

    public void insertcand_poste(int id_candiplome, int idposte)
    {
        id_candiplome = lastId();

        using (NpgsqlConnection connection = Connect.GetSqlConnection())
        {
            connection.Open();

            string query = "INSERT INTO candidat_poste VALUES(" + id_candiplome + "," + idposte + ")";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public List<Candidat> getAllCandidatSelectionne(int id_serP)
    {
        List<Candidat> serp = new List<Candidat>();

        using NpgsqlConnection connection = Connect.GetSqlConnection();
        connection.Open();
        // string query = "SELECT * FROM candidat_et_diplome WHERE diplome = (SELECT diplome FROM service_poste WHERE id = "+ id_serP + ") AND sexe = (SELECT sexe FROM service_poste WHERE id = " + id_serP + ") AND dtn >= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_f FROM service_poste WHERE id = " + id_serP + ")) AND dtn <= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_d FROM service_poste WHERE id = "+ id_serP + "))AND lieu = (SELECT lieu FROM service_poste WHERE id = " + id_serP + ")";
        string query = "SELECT c.nom_candidat,c.prenom_candidat,c.dtn,c.email,c.sexe,c.telephone,c.situation,c.adresse,c.region,c.province FROM candidat c JOIN candidat_diplome cd ON cd.id_candidat = c.id JOIN candidat_poste cp ON cp.id_cand_dip = cd.id WHERE cd.niveau_etude = (SELECT diplome FROM service_poste WHERE id ="+ id_serP +") AND c.sexe = (SELECT sexe FROM service_poste WHERE id ="+ id_serP +") AND (DATE_PART('year', CURRENT_DATE) - DATE_PART('year', c.dtn)) >= CAST((SELECT age_d FROM service_poste WHERE id ="+ id_serP +") AS INTEGER) AND (DATE_PART('year', CURRENT_DATE) - DATE_PART('year', c.dtn)) <= CAST((SELECT age_f FROM service_poste WHERE id ="+ id_serP +") AS INTEGER) AND c.province = (SELECT lieu FROM service_poste WHERE id = "+ id_serP +" limit (SELECT CAST((valeur_horaire/7*3/30) as INTEGER) FROM service_poste WHERE id = "+ id_serP +"))";
        Console.WriteLine(query);
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   Candidat c= new Candidat();
                   c.nom = reader.GetString(0);
                   c.prenom= reader.GetString(1);
                   c.email = reader.GetString(3);
                   c.telephone = reader.GetString(5);
                   serp.Add(c);
                }
            }
        }
        connection.Close();

        return serp;
    }
}