using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InfinityComics1.Auth.Models
{
    public class ComicBook
    { 
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int IssueNumber { get; set; }

        //[DisplayName("Published")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        //public DateTime? ReleaseDate { get; set; }

        [DisplayName("Date Added")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime? DateAdded { get; set; }

        //[Required]
        //[DisplayName("Author")]
        //public int AuthorId { get; set; }
        //public Author Author { get; set; }

        //[Required]
        //[DisplayName("User Profile")]
        //public int UserProfileId { get; set; }
        //public UserProfile UserProfile { get; set; }
    }
}
