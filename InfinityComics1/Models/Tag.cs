using System.ComponentModel.DataAnnotations;

namespace InfinityComics1.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TagName { get; set; }       
    }
}
