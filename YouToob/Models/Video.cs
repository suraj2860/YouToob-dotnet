using System.ComponentModel.DataAnnotations;

namespace YouToob.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required]
        public string VideoFile { get; set; }
        [Required]
        public string Thumbnail { get; set; }
        public User Owner { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Duration { get; set; }
        public int Views { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
