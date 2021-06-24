using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MovieAPI.Data;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _repository;

        // Noticed this should have been called "Movies", not Movie
        public MovieController(IMovieRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("stats")]
        public ActionResult<IEnumerable<MovieStats>> Get()
        {
            IEnumerable<MovieStats> Items = _repository.GetStats(); 

            if (Items != null && Items.Count() > 0)
                return Ok(Items);
            else
                return Problem(statusCode: 404, title: "No data");
        }


    }
}
