using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MovieAPI.Models;

namespace MovieAPI.Data
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();

        IEnumerable<Movie> GetMovieByLanguage(int id);

        Task<int> AddMovie(Movie data);

        IEnumerable<MovieStats> GetStats();
    }
}
