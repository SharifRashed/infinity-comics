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
        public List<ComicBook> GetAllComicBooks(int id)
        {
            using (SqlConnection conn = Connection)
            {   
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //go to the server to retrieve info
                    cmd.CommandText = @" 
                       SELECT Id, [Title], Description, IssueNumber, DateAdded, UserProfileId, AuthorId
                       FROM ComicBook 
                       WHERE UserProfileId = @userId
                      ";

                    cmd.Parameters.AddWithValue("@userId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                     ///after exectute data, return objects 

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
                     SELECT cb.Id, [Title], cb.Description, IssueNumber, DateAdded, Name, UserProfileId
                    FROM ComicBook cb
                    JOIN Author ON cb.AuthorId = Author.Id
                    WHERE cb.Id = @id
                    ";

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
                                Author = new Author()
                                {
                                    Name = DbUtils.GetString(reader, "Name"),
                                },
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

        public void Update(ComicBook comicBook)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE ComicBook
                            SET
                                [Title] = @title,
                                Description = @description,
                                IssueNumber = @issueNumber,
                                DateAdded = @dateAdded,
                                UserProfileId = @userProfileId,
                                AuthorId = @authorId                               
                            WHERE Id = @id
                               ";
                    cmd.Parameters.AddWithValue("@id", comicBook.Id);
                    cmd.Parameters.AddWithValue("@title", comicBook.Title);
                    cmd.Parameters.AddWithValue("@description", comicBook.Description);
                    cmd.Parameters.AddWithValue("@issueNumber", comicBook.IssueNumber);
                    cmd.Parameters.AddWithValue("@dateAdded", comicBook.DateAdded);
                    cmd.Parameters.AddWithValue("@userProfileId", comicBook.UserProfileId);
                    cmd.Parameters.AddWithValue("@authorId", comicBook.AuthorId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
