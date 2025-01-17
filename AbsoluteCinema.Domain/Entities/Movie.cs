using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsoluteCinema.Domain.Entities
{
    internal class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public double Score { get; set; }
        public bool Adult { get; set; }
        public string PosterPath { get; set; }
        public MovieLanguageEnum Language { get; set; }
        public DateTime ReleaseDate { get; set; } // New field, we have the data for it in TMDB
    }
}
