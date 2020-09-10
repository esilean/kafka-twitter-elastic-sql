using Microsoft.EntityFrameworkCore;

namespace Kafka.Consumer.MSSQL.Data
{
    public class TwitterDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=KafkaTwitter;User Id=sa;Password=pass123!;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shared.Models.Tweet>()
                    .Property(et => et.Id)
                    .ValueGeneratedNever();
        }

        public DbSet<Shared.Models.Tweet> Tweets { get; set; }
    }
}
