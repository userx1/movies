using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MovieAPI.Models;

namespace MovieAPI.Data
{
    public class MovieRepository : IMovieRepository
    {
        public IEnumerable<Movie> GetMovies() {

            List<Movie> Movies = new List<Movie>();

            return Movies;
        }

        public IEnumerable<Movie> GetMovieByLanguage(int id)
        {
            //Results are ordered alphabetically by language.
            List<Movie> Movies = new List<Movie>();

            // TODO: remove records where data is not complete!!!
            Movies = MovieDB.GetMovieListing().Where(c => c.movieId == id).ToList();
            return Movies.OrderBy(c => c.title).OrderBy(c => c.language).ToList();
        }

        public async Task<int> AddMovie(Movie data)
        {
            int RecordId = 1;

            await Task.Run(() => MovieDB.AddMovie(data));
            return RecordId;
        }

        public IEnumerable<MovieStats> GetStats() 
        {
            List<MovieStats> Stats = new List<MovieStats>();

            Stats = MovieDB.GetMovieStats();
            return Stats;
        }
    }
}
