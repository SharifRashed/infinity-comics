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
                return new SqlConnection(_config.GetConnectionString("DefualtConnection"));
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
                        FROM ComicBook  
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
    }
}
