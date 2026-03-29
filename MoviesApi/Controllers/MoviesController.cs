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
        public ActionResult<PagedResult<MovieReadDto>> GetMovies([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            int totalRecords = _repository.GetTotalCount();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var paginatedMovies = _repository.GetPaginated(pageNumber, pageSize)
                .Select(m => new MovieReadDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleaseYear = m.ReleaseYear,
                    Genre = m.Genre,
                    ImgUrl = m.ImgUrl
                });

            return Ok(new PagedResult<MovieReadDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Data = paginatedMovies
            });
        }

        /// <summary>
        /// Get a specific movie by Id
        /// </summary>
        /// <param name="id">Movie Id</param>
        [HttpGet("{id}")]
        public ActionResult<MovieReadDto> GetMovie(int id)
        {
            var movie = _repository.GetById(id);
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
        public ActionResult<MovieReadDto> CreateMovie(MovieCreateDto createDto)
        {
            var movie = new Movie
            {
                Title = createDto.Title,
                ReleaseYear = createDto.ReleaseYear,
                Genre = createDto.Genre,
                ImgUrl = createDto.ImgUrl
            };

            _repository.Add(movie);

            var readDto = new MovieReadDto
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                Genre = movie.Genre,
                ImgUrl = movie.ImgUrl
            };

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, readDto);
        }

        /// <summary>
        /// Fully update an existing movie
        /// </summary>
        /// <param name="id">Movie Id to update</param>
        /// <param name="updateDto">New movie details</param>
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, MovieUpdateDto updateDto)
        {
            var existingMovie = _repository.GetById(id);
            if (existingMovie == null)
            {
                return NotFound();
            }

            existingMovie.Title = updateDto.Title;
            existingMovie.ReleaseYear = updateDto.ReleaseYear;
            existingMovie.Genre = updateDto.Genre;
            existingMovie.ImgUrl = updateDto.ImgUrl;

            _repository.Update(existingMovie);

            return NoContent();
        }

        /// <summary>
        /// Delete a movie from the database
        /// </summary>
        /// <param name="id">Movie Id to delete</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var existingMovie = _repository.GetById(id);
            if (existingMovie == null)
            {
                return NotFound();
            }

            _repository.Delete(id);

            return NoContent();
        }
    }
}
