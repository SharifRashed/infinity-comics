using InfinityComics1.Auth.Models;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InfinityComics1.Repositories
{
    public class ComicBookRepository : IComicBookRepository
    {
        private readonly IConfiguration _config;
        public ComicBookRepository(IConfiguration config)
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
        public List<ComicBook> GetAllComicBooks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" 
                       SELECT Id, [Title], Description, IssueNumber, DateAdded, UserProfileId
                        FROM ComicBook  
                      ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {


                        List<ComicBook> comicBooks = new List<ComicBook>();

                        while (reader.Read())
                        {
                            ComicBook comicBook = new ComicBook
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                IssueNumber = reader.GetInt32(reader.GetOrdinal("IssueNumber")),
                                //ReleaseDate = reader.GetDate(reader.GetOrdinal("Id")),
                                DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            };
                            comicBooks.Add(comicBook);
                        }
                        return comicBooks;
                    }
                }
            }
        }

         public void AddComicBook(ComicBook comicBook)
        {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @" 
                       INSERT INTO ComicBook ([Title], Description, IssueNumber, ReleaseDate, DateAdded, UserProfileId)
                       OUTPUT INSERTED.Id
                       VALUES (@title, @description, @issueNumber, @releaseDate, @dateAdded, @userProfileId);
                        ";

                        cmd.Parameters.AddWithValue("@Id", comicBook.Id);
                        cmd.Parameters.AddWithValue("@Title", comicBook.Title);
                        cmd.Parameters.AddWithValue("@Description", comicBook.Description);
                        cmd.Parameters.AddWithValue("@IssueNumber", comicBook.IssueNumber);
                        cmd.Parameters.AddWithValue("@DateAdded", comicBook.DateAdded);
                        //cmd.Parameters.AddWithValue("@ReleaseDate", comicBook.ReleaseDate);
                        cmd.Parameters.AddWithValue("@UserProfileId", comicBook.UserProfileId);

                        int id = (int)cmd.ExecuteScalar();

                        comicBook.Id = id;
                    }
                }
         }
        
    }
}
