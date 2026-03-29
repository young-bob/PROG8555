using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Models.DTOs;
using MoviesApi.Repositories;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _repository;

        public MoviesController(IMovieRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get a paginated list of movies
        /// </summary>
        /// <param name="pageNumber">Page number to retrieve</param>
        /// <param name="pageSize">Number of items per page</param>
        [HttpGet]
        public async Task<ActionResult<PagedResult<MovieReadDto>>> GetMovies([FromQuery] int pageNumber = 1, 
                                                                             [FromQuery] int pageSize = 5)
        {
            int totalRecords = await _repository.Count();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var result = new PagedResult<MovieReadDto>
            { 
                PageNumber = pageNumber, 
                PageSize = pageSize, 
                TotalPages = totalPages, 
                TotalRecords = totalRecords
            };

            if(pageNumber > totalPages)
                return Ok(result);

            var movies = await _repository.GetPaginated(pageNumber, pageSize);
            result.Data = movies.Select(m => new MovieReadDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleaseYear = m.ReleaseYear,
                    Genre = m.Genre,
                    ImgUrl = m.ImgUrl
                });

            return Ok(result);
        }

        /// <summary>
        /// Get a specific movie by Id
        /// </summary>
        /// <param name="id">Movie Id</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDto>> GetMovie(int id)
        {
            var movie = await _repository.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(new MovieReadDto
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                Genre = movie.Genre,
                ImgUrl = movie.ImgUrl
            });
        }

        /// <summary>
        /// Create a new movie
        /// </summary>
        /// <param name="createDto">Movie details</param>
        [HttpPost]
        public async Task<ActionResult<MovieReadDto>> CreateMovie(MovieCreateDto createDto)
        {
            var movie = new Movie
            {
                Title = createDto.Title,
                ReleaseYear = createDto.ReleaseYear,
                Genre = createDto.Genre,
                ImgUrl = createDto.ImgUrl
            };

            var created = await _repository.Add(movie);
            if (created == null) return BadRequest();

            var readDto = new MovieReadDto
            {
                Id = created.Id,
                Title = created.Title,
                ReleaseYear = created.ReleaseYear,
                Genre = created.Genre,
                ImgUrl = created.ImgUrl
            };

            return CreatedAtAction(nameof(GetMovie), new { id = created.Id }, readDto);
        }

        /// <summary>
        /// Fully update an existing movie
        /// </summary>
        /// <param name="id">Movie Id to update</param>
        /// <param name="updateDto">New movie details</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDto updateDto)
        {
            var movie = new Movie
            {
                Id = id,
                Title = updateDto.Title,
                ReleaseYear = updateDto.ReleaseYear,
                Genre = updateDto.Genre,
                ImgUrl = updateDto.ImgUrl
            };

            var updated = await _repository.Update(movie);
            if (updated == null) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Delete a movie from the database
        /// </summary>
        /// <param name="id">Movie Id to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deleted = await _repository.Delete(id);
            if (deleted == null) return NotFound();

            return NoContent();
        }
    }
}
