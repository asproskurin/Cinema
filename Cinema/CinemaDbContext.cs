using Cinema.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Cinema
{
    public interface ICinemaDbContext
    {
        DbSet<FilmDto> Films { get; set; }
        DbSet<HallDto> Halls { get; set; }
        DbSet<SessionDto> Sessions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class CinemaDbContext(DbContextOptions<CinemaDbContext> options) : DbContext(options), ICinemaDbContext
    {
        public DbSet<FilmDto> Films { get; set; }
        public DbSet<HallDto> Halls { get; set; }
        public DbSet<SessionDto> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmDto>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Year)
                    .IsRequired();

                entity.Property(c => c.Duration)
                    .IsRequired();

                entity.Property(c => c.AgeLimit)
                    .IsRequired();

                entity.Property(c => c.Poster)
                   .IsRequired();

                entity.Property(c => c.IsActive)
                   .IsRequired();

                entity.HasIndex(c => c.Name)
                    .IsUnique();
            });

            modelBuilder.Entity<HallDto>(entity =>
            {
                entity.HasKey(ch => ch.Id);

                entity.Property(ch => ch.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(ch => ch.Quantity)
                    .IsRequired();

                entity.Property(ch => ch.BreakTime)
                    .IsRequired();
            });

            modelBuilder.Entity<SessionDto>(entity =>
            {
                entity.HasKey(cs => cs.Id);

                entity.Property(cs => cs.FilmId)
                    .IsRequired();

                entity.Property(cs => cs.HallId)
                    .IsRequired();

                entity.Property(cs => cs.StartTime)
                    .IsRequired();

                entity.Property(cs => cs.Cost)
                    .IsRequired();

                entity.Property(cs => cs.Status)
                    .IsRequired();


                entity.HasOne(cs => cs.Hall)
                    .WithMany(ch => ch.Sessions)
                    .HasForeignKey(cs => cs.HallId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(cs => new { cs.HallId, cs.StartTime })
                        .IsUnique();
            });

            modelBuilder.Entity<FilmDto>().HasQueryFilter(f => f.IsActive);
            modelBuilder.Entity<SessionDto>().HasQueryFilter(cs => cs.Status);
        }
    }
}
