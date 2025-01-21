using AbsoluteCinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbsoluteCinema.Infrastructure.EntitiesConfiguration
{
    public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasKey(mg => new {mg.MovieId, mg.GenreId});

            // Relations with table Movie
            builder.HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenre)
                .HasForeignKey(mg => mg.MovieId)
                .IsRequired();

            // Relations with table Genre
            builder.HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenre)
                .HasForeignKey(mg => mg.GenreId)
                .IsRequired();
        }
    }
}
