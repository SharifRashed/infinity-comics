using InfinityComics1.Auth.Models;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using InfinityComics1.Utils;

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
                            ComicBook comicBook = new ComicBook()
                            {
                                Id = DbUtils.GetInt(reader,"Id"),
                                Title = DbUtils.GetString(reader,"Title"),
                                Description = DbUtils.GetString(reader,"Description"),
                                IssueNumber = DbUtils.GetInt(reader,"IssueNumber"),
                                //ReleaseDate = DbUtils.GetDate(reader,"Id"),
                                DateAdded = DbUtils.GetDateTime(reader,"DateAdded"),
                                UserProfileId = DbUtils.GetInt(reader,"UserProfileId"),
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

                    //DbUtils.AddParameter(cmd, "@Id", comicBook.id);
                       
                        DbUtils.AddParameter(cmd, "@Title", comicBook.Title);
                        DbUtils.AddParameter(cmd, "@Description", comicBook.Description);
                        DbUtils.AddParameter(cmd, "@IssueNumber", comicBook.IssueNumber);
                        DbUtils.AddParameter(cmd, "@DateAdded", comicBook.DateAdded);
                    //DbUtils.AddParameter(cmd, "@ReleaseDate", comicBook.ReleaseDate);
                        DbUtils.AddParameter(cmd, "@UserProfileId", comicBook.UserProfileId);

                    int id = (int)cmd.ExecuteScalar();

                    comicBook.Id = id;
                }
                }
         }
        
    }
}
