using Npgsql;

namespace TOVO.Models{
    public class Service {
        public int id { get; set; }
        public string nom_service { get; set; }

        public Service(int id, string nom_service){
            this.id = id;
            this.nom_service = nom_service;
        }

        public Service(){}
        public List<Service> GetAllService() {
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

        // public List<Poste> GetAllPoste(String nom_service)
        // {
        //     List<Poste> postes = new List<Poste>();

        //     using NpgsqlConnection connection = Connect.GetSqlConnection();
        //     connection.Open();
        //     string query = "SELECT * FROM ser_pos where nom_service = '"+ nom_service+"'";
        //     using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        //     {
        //         using (NpgsqlDataReader reader = command.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 Poste post = new Poste();
        //                 post.id = reader.GetInt32(0);
        //                 post.nom_poste = reader.GetString(1);
        //                 post.id_serv = reader.GetInt32(2);
        //                 postes.Add(post);
        //             }
        //         }
        //     }
        //     connection.Close();
        //     return postes;
        // }
    }
}