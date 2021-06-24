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
    [ApiController]
    public class MetadataController : ControllerBase
    {
        private readonly IMovieRepository _repository;

        // Constructor to allow using dependency injection.
        public MetadataController(IMovieRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Movie>> Get(int id)
        {
            IEnumerable<Movie> Items = _repository.GetMovieByLanguage(id); 

            if (Items != null && Items.Count() > 0)
                return Ok(Items);
            else
                return Problem(statusCode: 404, title: "No data");
        }


        [HttpPost]
        public async Task<IActionResult> Post(Movie movie)
        {
            bool AddOk = false;

            try
            {

                int RecordId = await _repository.AddMovie(movie);

                if (RecordId > 0)
                    AddOk = true;
            }
            catch (Exception ex)
            {
                AddOk = false;
                // Add error logging here
            }

            if (AddOk)
                return Ok();            
            else
                return Problem(statusCode: 500, title: "System was unable to process the record");
        }



    }
}
