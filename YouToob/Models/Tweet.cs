using System.ComponentModel.DataAnnotations;

namespace YouToob.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public User Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
