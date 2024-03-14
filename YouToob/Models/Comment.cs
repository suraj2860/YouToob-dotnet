using System.ComponentModel.DataAnnotations;

namespace YouToob.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public User Owner { get; set; }
        public Video Video { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
