using System.ComponentModel.DataAnnotations;

namespace YouToob.Models
{
    public class Like
    {
        public int Id { get; set; }
        public Video Video { get; set; }
        public Comment Comment { get; set; }
        public Tweet Tweet { get; set; }
        public User LikedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
