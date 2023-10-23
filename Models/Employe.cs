using Npgsql;
using Npgsql.Replication;

namespace TOVO.Models;

public class Employe
{
    public int id { get; set; }
    public string nom { get; set; }
    public string prenom { get; set; }
    public DateTime date_de_naissance { get; set; }
    public string genre { get; set; }
    
    public Employe() { }

    public void Insert_Employe(String nom, String prenom, DateTime dtn, String genre)
    {

        using (NpgsqlConnection connection = Connect.GetSqlConnection())
        {
            connection.Open();

            string query = "INSERT INTO employe VALUES( default ,'" + nom + "','" + prenom + "','" + genre + "','" + dtn + "')";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public void Insert_embauche(int id_emp, String nom_poste, String numero_asa, double salaire)
    {

        using (NpgsqlConnection connection = Connect.GetSqlConnection())
        {
            connection.Open();

            string query = "INSERT INTO embauche VALUES( default ," + id_emp + ",'"+ nom_poste +"', CURRENT_DATE,'" + numero_asa + "', "+ salaire +")";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public List<Employe> getAllEmploye()
    {
        List<Employe> serp = new List<Employe>();

        using NpgsqlConnection connection = Connect.GetSqlConnection();
        connection.Open();
        // string query = "SELECT * FROM candidat_et_diplome WHERE diplome = (SELECT diplome FROM service_poste WHERE id = "+ id_serP + ") AND sexe = (SELECT sexe FROM service_poste WHERE id = " + id_serP + ") AND dtn >= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_f FROM service_poste WHERE id = " + id_serP + ")) AND dtn <= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_d FROM service_poste WHERE id = "+ id_serP + "))AND lieu = (SELECT lieu FROM service_poste WHERE id = " + id_serP + ")";
        string query = "SELECT * FROM employe";

        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Employe e = new Employe();
                    e.id = reader.GetInt32(0);
                    e.nom = reader.GetString(1);
                    e.prenom = reader.GetString(2);
                    e.date_de_naissance = reader.GetDateTime(3);
                    e.genre= reader.GetString(4);
                    serp.Add(e);
                }
            }
        }
        connection.Close();

        return serp;
    }

    public int id_emp(String nom, String prenom)
    {
        int id = 0;

        using NpgsqlConnection connection = Connect.GetSqlConnection();
        connection.Open();
        // string query = "SELECT * FROM candidat_et_diplome WHERE diplome = (SELECT diplome FROM service_poste WHERE id = "+ id_serP + ") AND sexe = (SELECT sexe FROM service_poste WHERE id = " + id_serP + ") AND dtn >= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_f FROM service_poste WHERE id = " + id_serP + ")) AND dtn <= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_d FROM service_poste WHERE id = "+ id_serP + "))AND lieu = (SELECT lieu FROM service_poste WHERE id = " + id_serP + ")";
        string query = "SELECT id FROM employe where nom ='"+nom+"' AND prenom = '"+prenom+"'";

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
}