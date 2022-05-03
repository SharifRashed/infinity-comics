using InfinityComics1.Models;
using InfinityComics1.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace InfinityComics1.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IConfiguration _config;
        public AuthorRepository(IConfiguration config)
        {
            _config = config;
        }
        
        public SqlConnection Connection
        {
            get 
            { 
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public List<Author> GetAllAuthors()
    {
            using (SqlConnection conn = Connection)
        {
                conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @" 
                       SELECT Id, [Name], Description
                        FROM Author  
                      ";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {


                    List<Author> authors = new List<Author>();

                    while (reader.Read())
                    {
                        Author author = new Author()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Description = DbUtils.GetString(reader, "Description"),                          
                        };
                        authors.Add(author);
                    }
                    return authors;
                }
            }
        }

    }
    }
}
