using InfinityComics1.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InfinityComics1.Auth.Models
{
    public class ComicBook
    { 
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int IssueNumber { get; set; }
        public DateTime? DateAdded { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
