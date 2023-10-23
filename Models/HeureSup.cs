using Npgsql;
using Npgsql.Replication;

namespace TOVO.Models;

public class HeureSup
{
 public Embauchers employe {get; set;}
 public int nombre {get; set;}
 
 
 public HeureSup(){}

    public int nombre_heure(String numero_employe)
    {
        int heure = 0;
        using NpgsqlConnection connection = Connect.GetSqlConnection();
        connection.Open();
        string query = "SELECT nombre_heure FROM hs_emp where numero_employe = '"+numero_employe+"'";

        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    heure = reader.GetInt32(0);
                }
            }
        }
        connection.Close();
        return heure;
    }

     public Embauchers getemploye_embaucher(String numero_employe)
    {
        Embauchers e = new Embauchers();

        using NpgsqlConnection connection = Connect.GetSqlConnection();
        connection.Open();
        string query = "SELECT * FROM employe_embaucher where numero_employe = '"+ numero_employe +"'";

        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    e.id = reader.GetInt32(0);
                    e.poste = reader.GetString(2);
                    e. date_embauche = reader.GetDateTime(3);
                    e.numero_employe = reader.GetString(4);
                    e.salaire = reader.GetDouble(5);
                    e.nom = reader. GetString(6);
                    e.prenom = reader.GetString(7);
                    e.genre = reader.GetString(8);
                    e.date_de_naissance = reader.GetDateTime(9);
                }
            }
        }
        connection.Close();

        return e;
    }

    public double Calcul_heure_sup_81(String numero_employe){
        Embauchers e = getemploye_embaucher(numero_employe);
        double salaire = e.salaire;
        int nh = nombre_heure(numero_employe);
        double taux = 0.0;

        if(nh <= 8){
            taux = ((salaire/30)/24) * 1.3;
        }

        return taux;
    }
 



    
}