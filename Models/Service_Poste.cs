using Npgsql;

namespace TOVO.Models
{
    public class Service_Poste{

        public List<Service> services { get; set; }
        public List<Poste> postes { get; set; }

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

        public List<Poste> GetAllPoste()
        {
            List<Poste> postes = new List<Poste>();

            using NpgsqlConnection connection = Connect.GetSqlConnection();
            connection.Open();
            string query = "SELECT * FROM Poste";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Poste post = new Poste();
                        post.id = reader.GetInt32(0);
                        post.nom_poste = reader.GetString(1);
                        post.id_serv = reader.GetInt32(2);
                        postes.Add(post);
                    }
                }
            }
            connection.Close();
            return postes;
        }


        // public List<String> GetAllDiplome()
        // {
        //     List<String> diplomes = new List<String>();

        //     using NpgsqlConnection connection = Connect.GetSqlConnection();
        //     connection.Open();
        //     string query = "SELECT column_name FROM information_schema.columns WHERE table_name = '{note_diplome}' AND ordinal_position IN (2,3,5, 5, 6)";
        //     using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        //     {
        //         using (NpgsqlDataReader reader = command.ExecuteReader())
        //         {
        //             Liste<String> s = 
        //         }
        //     }
        //     connection.Close();
        //     return diplomes;
        // }

    }
}