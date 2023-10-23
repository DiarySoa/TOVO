using System.Data;
using Npgsql;

namespace TOVO.Models
{
    public class Service_Poste{

        public List<Service> services { get; set; }
        public List<Poste> postes { get; set; }
        public int id { get; set; }
        public string nom_service { get; set; }
        public double valeur { get; set; }
        public string nom_poste { get; set; }
        public string diplome { get; set; }
        public string sexe { get; set; }
        public string age_d { get; set; }
        public string age_f { get; set; }
        public string lieu { get; set; }
        public int personne { get; set; }



        public double valeur_heure_olona = 8;

        public List<Service> GetAllService()
        {
            List<Service> services = new List<Service>();

            using NpgsqlConnection connection = Connect.GetSqlConnection();
            connection.Open();
            string query = "SELECT * FROM Service";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Service serv = new Service();
                        serv.id = reader.GetInt32(0);
                        serv.nom_service = reader.GetString(1);
                        services.Add(serv);
                    }
                }
            }
            connection.Close();
            return services;
        }

        public List<String> GetAllPoste(String nom_service)
        {
            List<String> postes = new List<String>();

            using NpgsqlConnection connection = Connect.GetSqlConnection();
            connection.Open();
            string query = "SELECT nom_poste FROM ser_pos where nom_service = '" + nom_service + "'";
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



        public int GetIdService( String nom){
            int idcf = 0;
            using NpgsqlConnection connection = Connect.GetSqlConnection();
            connection.Open();
            string query = "SELECT id FROM Service WHERE nom_service='" + nom + "'";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        idcf = reader.GetInt32(0);
                    }
                }
            }
            connection.Close();
            return idcf;
        }

        public int GetIdPoste(String nom)
        {

            int idcf = 0;
            using NpgsqlConnection connection = Connect.GetSqlConnection();
            connection.Open();
            string query = "SELECT id FROM poste WHERE nom_poste ='" + nom + "'";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        idcf = reader.GetInt32(0);
                    }
                }
            }
            connection.Close();
            return idcf;
        }

        public String GetNomService(int id)
        {
            String idcf = "";
            using NpgsqlConnection connection = Connect.GetSqlConnection();
            connection.Open();
            string query = "SELECT nom_service FROM Service WHERE id=" + id ;
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        idcf = reader.GetString(0);
                    }
                }
            }
            connection.Close();
            return idcf;
        }

        public String GetNomPoste(int id)
        {

            String idcf = "";
            using NpgsqlConnection connection = Connect.GetSqlConnection();
            connection.Open();
            string query = "SELECT nom_poste FROM Poste WHERE id=" + id;
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        idcf = reader.GetString(0);
                    }
                }
            }
            connection.Close();
            return idcf;

        }


        public void InsertSP(int nom_service, int nom_poste, double valeur, String diplome, String sexe, String age_d, String age_f, String lieu)
        {
            int id_service = nom_service;
            int id_poste = nom_poste;
            
            using (NpgsqlConnection connection = Connect.GetSqlConnection())
            {
                connection.Open();

                string query = "INSERT INTO service_poste VALUES( default ," + id_service + "," + valeur + "," + id_poste + ",'" + diplome +"','"+ sexe+"','"+age_d+"','"+age_f+"','"+lieu +"')";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // public double isanolona(double valeur){
        //     double isa = valeur / this.valeur_heure_olona;
        //     return isa ;
        // }

        public Service_Poste last(){
            Service_Poste sp = new Service_Poste();

            using NpgsqlConnection connection = Connect.GetSqlConnection();
            connection.Open();
            string query = "SELECT * FROM service_poste ORDER BY id DESC limit 1";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        sp.id= reader.GetInt32(0);
                        sp.nom_service = GetNomService(reader.GetInt32(1));
                        sp.nom_poste = GetNomPoste(reader.GetInt32(3));
                        sp.diplome = reader.GetString(4);
                        sp.personne = (int) (reader.GetDouble(2)/7);

                    }
                }
            }
            connection.Close();

            return sp;
        }

        public List<Service_Poste> getAllServicePoste()
        {
            List<Service_Poste> serp = new List<Service_Poste>();

            using NpgsqlConnection connection = Connect.GetSqlConnection();
            connection.Open();
            string query = "SELECT * FROM service_poste";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Service_Poste sp = new Service_Poste();
                        sp.id = reader.GetInt32(0);
                        sp.nom_service = GetNomService(reader.GetInt32(1));
                        sp.nom_poste = GetNomPoste(reader.GetInt32(3));
                        sp.diplome = reader.GetString(4);
                        sp.sexe = reader.GetString(5);
                        sp.age_d = reader.GetString(6);
                        sp.age_f = reader.GetString(7);
                        // sp.lieu = reader.GetString(8);
                        if (!reader.IsDBNull(8))
                        {
                            sp.lieu = reader.GetString(8);
                        }
                        else
                        {
                            // Gérez le cas où la colonne "lieu" est NULL
                            sp.lieu = null; // Ou toute autre valeur par défaut que vous souhaitez
                        }
                        // sp.personne = (int)(reader.GetDouble(2) / 7);
                        serp.Add(sp);

                    }
                }
            }
            connection.Close();

            return serp;
        }

        

        }
}