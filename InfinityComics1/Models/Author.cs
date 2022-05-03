using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InfinityComics1.Models
{
    public class Author
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]

        public string Description { get; set; }
    }
}
