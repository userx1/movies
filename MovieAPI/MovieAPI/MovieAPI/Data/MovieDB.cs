using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Globalization;

using MovieAPI.Models;

namespace MovieAPI.Data
{
    public class MovieDB
    {
        public static List<Movie> GetMovieListing()
        {
            List<Movie> Movies = new List<Movie>();

            String FilePath = Directory.GetCurrentDirectory() + "\\Data\\Database\\metadata.csv";

            if (File.Exists(FilePath))
            {
                //Id,MovieId,Title,Language,Duration,ReleaseYear
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ",",
                    //HeaderValidated = null,
                    PrepareHeaderForMatch = args => args.Header.ToLower(),

                };

                using (var reader = new StreamReader(FilePath))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        Movies.Add(csv.GetRecord<Movie>());
                    }
                }

            }

            return Movies;

        }

        private static List<Movie> GetDistinctMovieIds()
        {
            List<Movie> Movies = new List<Movie>();
            List<Movie> MoviesList = GetMovieListing();

            Movies = MoviesList.GroupBy(c => c.movieId)
                                .Select(cl => new Movie
                                {
                                    movieId = cl.Select(c => c.movieId).FirstOrDefault(),
                                    title = cl.Select(c => c.title).FirstOrDefault(),
                                    releaseYear = cl.Select(c => c.releaseYear).FirstOrDefault()  // This should really have checks around it.
                                }).ToList();
            return Movies;
        }

        // public int movieId { get; set; }
        // public string title { get; set; }
        // public int averageWatchDurationS { get; set; }
        // public int watches { get; set; }
        // public int sreleaseYear { get; set; }
        public static List<MovieStats> GetMovieStats()
        {
            List<MovieStats> Stats = new List<MovieStats>();
            List<RawMovieStats> RawStats = GetRawMovieStats();
            List<Movie> Movies = GetDistinctMovieIds();

            if (RawStats != null && RawStats.Count > 0)
            {
                List<KeyValuePair<int, int>> data = new List<KeyValuePair<int, int>>();

                List<int> Ids = RawStats.Select(c => c.movieId).Distinct().ToList();

                foreach (int i in Ids)
                {
                    var temp  = RawStats.Where(c => c.movieId == i).ToList();
                    int count = temp.Count();
                    float number = 0;

                    foreach (int ii in temp.Select(c => c.watchDurationMs).ToList())
                        number = number + ii;

                    float total = number / count;
                    int iTotal = Convert.ToInt32(total);
                    Stats.Add(new MovieStats() { movieId = i, averageWatchDurationS = iTotal, watches = count });
                }
            }

            List<MovieStats> FinalStats = Movies.Join(Stats, movie => movie.movieId, stats => stats.movieId, (movie, stats) => new MovieStats { 
                movieId = movie.movieId,
                title = movie.title,
                sreleaseYear = movie.releaseYear,
                averageWatchDurationS = stats.averageWatchDurationS,
                watches = stats.watches
            }).ToList(); 

            return FinalStats;
        }

        private static List<RawMovieStats> GetRawMovieStats()
        {
            List<RawMovieStats> Stats = new List<RawMovieStats>();
            String FilePath = Directory.GetCurrentDirectory() + "\\Data\\Database\\stats.csv";

            if (File.Exists(FilePath))
            {
                String[] records = File.ReadAllLines(FilePath);
                records = records.Skip(1).ToArray();                // Ignore headers

                foreach (string item in records)
                {
                    string[] values = item.Split(',');

                    Stats.Add(new RawMovieStats()
                    {
                        movieId = Convert.ToInt32(values[0]),
                        watchDurationMs = Convert.ToInt32(values[1])
                    });
                }
            }

            return Stats;

        }

        private static List<MovieStats> GenerateStatsByMovie()
        {
            List<MovieStats> Stats = new List<MovieStats>();
            List<RawMovieStats> RawStats = GetRawMovieStats();

            if (RawStats != null && RawStats.Count > 0)
            {
                Stats = RawStats.GroupBy(c => c.movieId)
                                .Select(cl => new MovieStats
                                {
                                    movieId = cl.Select(c => c.movieId).FirstOrDefault(),
                                    watches = cl.Count(),
                                    averageWatchDurationS = cl.Sum(c => c.watchDurationMs) / cl.Count()  // This should really have checks around it.
                                }).ToList();
            }

            return Stats;
        }

        public static int AddMovie(Movie data)
        {
            int RecordId = 1;
            // Add new record in file - TODO 
            
            return RecordId;
        }
    }
}
