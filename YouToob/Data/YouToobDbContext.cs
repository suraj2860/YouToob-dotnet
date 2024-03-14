using Microsoft.EntityFrameworkCore;
using YouToob.Models;

namespace YouToob.Data
{
    public class YouToobDbContext(DbContextOptions<YouToobDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
        public override int SaveChanges()
        {
            var currentTime = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = currentTime;
                }
                entry.Property("UpdatedAt").CurrentValue = currentTime;

                if (entry.Entity is User user)
                {
                    // Trim and convert usernames to lowercase before saving changes
                    user.Username = user.Username?.Trim().ToLower();

                    // Hash password if it's added or modified
                    if (!string.IsNullOrWhiteSpace(user.Password) && entry.State == EntityState.Added)
                    {
                        string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
