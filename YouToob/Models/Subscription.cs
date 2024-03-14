namespace YouToob.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public User Subscriber { get; set; }
        public User Channel { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
