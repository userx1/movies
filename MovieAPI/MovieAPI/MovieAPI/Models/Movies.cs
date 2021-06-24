using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;

namespace MovieAPI.Models
{
    //Example:
    //{
    //"movieId": 3,
    //"title": "Elysium",
    //"language": "EN",
    //"duration": "1:49:00",
    //"releaseYear": 2013
    //}
    public class Movie
    {
        public int id { get; set; }
        public int movieId { get; set; }
        public string title { get; set; }
        public string language { get; set; }
        public string duration { get; set; }
        public int releaseYear { get; set; }
    }

    //{
    //"movieId": 3,
    //"title": "Elysium",
    //"averageWatchDurationS": 3600,
    //"watches": 4000,
    //"releaseYear": 2013
    //},    
    public class MovieStats
    {
        public int movieId { get; set; }
        public string title { get; set; }
        public int averageWatchDurationS { get; set; }
        public int watches { get; set; }
        public int sreleaseYear { get; set; }
    }

    public class RawMovieStats
    { 
        public int movieId { get; set; }
        public int watchDurationMs { get; set; }
    }


}
