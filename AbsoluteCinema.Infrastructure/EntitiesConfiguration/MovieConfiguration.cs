using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Infrastructure.EntitiesConfiguration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(255);
            builder.Property(m => m.Discription).HasMaxLength(2047);
            builder.Property(m => m.Score);
            builder.Property(m => m.Adult).IsRequired();
            builder.Property(m => m.PosterPath);
            builder.Property(m => m.Language)
                  .HasConversion(
                      l => l.ToString(),
                      l => (MovieLanguageEnum)Enum.Parse(typeof(MovieLanguageEnum), l))
                  .IsRequired();
            builder.Property(m => m.ReleaseDate);

            // Relations with table MovieGenre
            builder.HasMany(m => m.MovieGenre)
                .WithOne(mg => mg.Movie)
                .HasForeignKey(mg => mg.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relations with table MovieActor
            builder.HasMany(m => m.MovieActor)
                .WithOne(ma => ma.Movie)
                .HasForeignKey(ma => ma.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relations with table Session
            builder.HasMany(m => m.Sessions)
                .WithOne(s => s.Movie)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
