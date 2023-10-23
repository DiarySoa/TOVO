using Npgsql;
using System.Data;

namespace TOVO.Models
{
public class Post
{
    public int id { get; set; }

    public string nom_poste { get; set; }

    public int id_ser { get; set; }

    public static List<Post> getByServ(int id_ser)
    {
        List<Post> posts = new List<Post>();
        using NpgsqlConnection connection = Connect.GetSqlConnection();
            connection.Open();
            string query = "SELECT * FROM poste where id_ser = " + id_ser + "";
            Console.WriteLine(query);
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Post post = new Post();
                        post.id = reader.GetInt32(0);
                        post.nom_poste = reader.GetString(1);
                        post.id_ser = reader.GetInt32(2);
                        posts.Add(post);
                    }
                }
            }
            connection.Close();
            Console.WriteLine(posts.Count());
        return posts;
    }
}
}
