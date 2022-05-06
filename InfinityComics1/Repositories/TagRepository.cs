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
    public class TagRepository : ITagRepository
    {
        private readonly IConfiguration _config;

        public TagRepository(IConfiguration config)
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

        //create a method to insert data to comicTag (comic Id, tag Id)

        public void SaveComicTag(int TagId , int ComicBookId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO ComicTag (TagId, ComicBookId)
                    OUTPUT Inserted.Id
                    VALUES ( @TagId, @ComicBookId)
                        ";
                    DbUtils.AddParameter(cmd, "@TagId", TagId);
                    DbUtils.AddParameter(cmd, "@ComicBookId", ComicBookId);
                    var reader = cmd.ExecuteScalar();
                }
            }
        }
        public List<Tag> GetAllTags()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" 
                       SELECT Id, [TagName]
                        FROM Tag  
                      ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {


                        List<Tag> tags = new List<Tag>();

                        while (reader.Read())
                        {
                            Tag tag = new Tag()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                TagName = DbUtils.GetString(reader, "TagName"),
                               
                            };
                            tags.Add(tag);
                        }
                        return tags;
                    }
                }
            }
        }

        public Tag GetTagById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, [TagName]
                    FROM Tag
                    WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Tag tag = new Tag()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                TagName = DbUtils.GetString(reader, "TagName"),                             
                            };
                            return tag;
                        }
                        return null;
                    }
                }
            }
        }

        public void AddTag(Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" 
                       INSERT INTO ComicTag ([TagName])
                       OUTPUT INSERTED.Id
                       VALUES (@TagName,);
                        ";

               

                    int id = (int)cmd.ExecuteScalar();
                    //post.Id = (int)cmd.ExecuteScalar();

                    tag.Id = id;
                }
            }
        }
    }
}
