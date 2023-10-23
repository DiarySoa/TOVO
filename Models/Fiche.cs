using Npgsql;
using Npgsql.Replication;

namespace TOVO.Models;

public class Fiche
{
 public Embauchers employe {get; set;}
 public double pourcentage_heure_sup {get; set;}
 public double pourcentage_cnaps {get; set;}
 
 public Fiche(){}

    public List<String> numero_des_employes_existant()
    {
        List<String> serp = new List<String>();

        using NpgsqlConnection connection = Connect.GetSqlConnection();
        connection.Open();
        string query = "SELECT numero_employe FROM employe_embaucher";

        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    String e =  reader.GetString(0);
                    serp.Add(e);
                }
            }
        }
        connection.Close();

        return serp;
    }


            public List<String> GetAllPoste()
        {
            List<String> postes = new List<String>();

            using NpgsqlConnection connection = Connect.GetSqlConnection();
            connection.Open();
            string query = "SELECT nom_poste FROM ser_pos";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        postes.Add(reader.GetString(0));
                    }
                }
            }
            connection.Close();
            return postes;
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

/////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public DateTime date_embauche_employe(String numero_employe)
    {
        DateTime e = new DateTime();

        using NpgsqlConnection connection = Connect.GetSqlConnection();
        connection.Open();
        string query = "SELECT date_embauche FROM employe_embaucher where numero_employe = '"+ numero_employe +"'";

        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    e = reader.GetDateTime(0);
                }
            }
        }
        connection.Close();

        return e;
    }

    

     public String CalculerAnciennete(String numero_employe){

        DateTime dateEmbauche = date_embauche_employe(numero_employe);
        DateTime dateActuelle = DateTime.Now;
        TimeSpan difference = dateActuelle - dateEmbauche;

        int moisAnciennete = (dateActuelle.Year - dateEmbauche.Year) * 12 + dateActuelle.Month - dateEmbauche.Month;
        int joursAnciennete = difference.Days;

        return $"{moisAnciennete} mois et {joursAnciennete} jours";
    }

    public double Calcul_cnaps(String numero_employe){
        Embauchers pers = getemploye_embaucher(numero_employe);

        double cnaps = 0.0;
        double salaire = pers.salaire;
        double sme = 238800;
        double plafond = sme * 8 ;

        if ( salaire > plafond )
        {
            cnaps = plafond * 0.01;
        }else{
            cnaps = salaire * 0.01;
        }

        return cnaps;

    }


       public double Calcul_ostie(String numero_employe){
        Embauchers pers = getemploye_embaucher(numero_employe);

        double ostie = 0.0;
        double salaire = pers.salaire;
        double sme = 238800;
        double plafond = sme * 8 ;

        ostie = salaire * 0.01;
        return ostie;

    }



        public double Calcul_IRSA_inf_350 (String numero_employe, Double base_imposable){
        Embauchers pers = getemploye_embaucher(numero_employe);

        double salaire = pers.salaire;
        // double base_imposable = salaire - cnaps_ostie;

        double IRSA = 0.0;
        
        if(base_imposable < 350000){
            IRSA = 0.0;
        }
        
        return IRSA;
    }


        public double Calcul_IRSA_btw_350_400 (String numero_employe, Double base_imposable){
        Embauchers pers = getemploye_embaucher(numero_employe);

        double salaire = pers.salaire;
        // double base_imposable = salaire - cnaps_ostie;

        double IRSA = 0.0;
        if(base_imposable > 350000 && base_imposable < 400000){
            IRSA = (base_imposable - 350000) * 0.05;
        }else{
            IRSA = (400000 - 350000) * 0.05;
            // IRSA = 0.0;
        }
        if(base_imposable < 350000){
            IRSA = 0.0;
        }
        return IRSA;

    }


        public double Calcul_IRSA_btw_400_500 (String numero_employe, Double base_imposable){
        Embauchers pers = getemploye_embaucher(numero_employe);

        double salaire = pers.salaire;
        // double base_imposable = salaire - cnaps_ostie;
        double IRSA = 0.0;

        if(base_imposable > 400000 && base_imposable < 500000){
            IRSA = (base_imposable - 400000) * 0.1;
        }else{
            IRSA = (500000 - 400000) * 0.1;
        }
        if(base_imposable < 400000){
            IRSA = 0.0;
        }

        return IRSA;
    }


        public double Calcul_IRSA_btw_500_600 (String numero_employe, Double base_imposable){
        Embauchers pers = getemploye_embaucher(numero_employe);

        double salaire = pers.salaire;
        // double base_imposable = salaire - cnaps_ostie;

        double IRSA = 0.0;
        if(base_imposable > 500000 && base_imposable < 600000){
            IRSA = (base_imposable - 500000) * 0.15;
        }else{
            IRSA = (600000 - 500000) * 0.15;
        }
        if(base_imposable < 500000){
            IRSA = 0.0;
        }


        return IRSA;
    }


        public double Calcul_IRSA_sup_600 (String numero_employe, Double base_imposable){
        Embauchers pers = getemploye_embaucher(numero_employe);

        double salaire = pers.salaire;
        // double base_imposable = salaire - cnaps_ostie;

        double IRSA = 0.0;
        
        if(base_imposable > 600000){
            IRSA = (base_imposable - 600000) * 0.2;
            
        }
        // else{
        //     IRSA = 0.0;
        // }
        
        return IRSA;
    }

    /////////////////////////////////////////////////////////////////////////////

  



 
}