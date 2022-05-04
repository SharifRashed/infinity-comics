using InfinityComics1.Models;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using InfinityComics1.Utils;
using Microsoft.AspNetCore.Mvc;

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
                       SELECT Id, [Title], Description, IssueNumber, DateAdded, UserProfileId, AuthorId
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

        public ComicBook GetComicBookById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, [Title], Description, IssueNumber, DateAdded, AuthorId, UserProfileId
                    FROM ComicBook
                    WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader =cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ComicBook comicBook = new ComicBook()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Description = DbUtils.GetString(reader, "Description"),
                                IssueNumber = DbUtils.GetInt(reader, "IssueNumber"),
                                //ReleaseDate = DbUtils.GetDate(reader,"Id"),
                                DateAdded = DbUtils.GetDateTime(reader, "DateAdded"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            };
                            return comicBook;
                        }
                        return null;
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
                       INSERT INTO ComicBook ([Title], Description, IssueNumber, DateAdded, UserProfileId, AuthorId)
                       OUTPUT INSERTED.Id
                       VALUES (@title, @description, @issueNumber, @dateAdded, @userProfileId, @AuthorId);
                        ";                 
                       
                        DbUtils.AddParameter(cmd, "@Title", comicBook.Title);
                        DbUtils.AddParameter(cmd, "@Description", comicBook.Description);
                        DbUtils.AddParameter(cmd, "@IssueNumber", comicBook.IssueNumber);
                        DbUtils.AddParameter(cmd, "@DateAdded", comicBook.DateAdded);
                    //DbUtils.AddParameter(cmd, "@ReleaseDate", comicBook.ReleaseDate);
                        DbUtils.AddParameter(cmd, "@UserProfileId", comicBook.UserProfileId);
                        DbUtils.AddParameter(cmd, "@AuthorId", comicBook.AuthorId);

                    int id = (int)cmd.ExecuteScalar();
                    //post.Id = (int)cmd.ExecuteScalar();

                    comicBook.Id = id;
                }
                }
         }    

        public void Delete(int comicBook)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    DELETE FROM ComicBook 
                    WHERE Id = @comicBook"
                    ;
                    cmd.Parameters.AddWithValue("@comicBook", comicBook);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
