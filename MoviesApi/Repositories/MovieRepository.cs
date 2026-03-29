using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;

namespace MoviesApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesDbContext _context;
        private static bool _inited = false;
        private static readonly object _initLock = new();

        public MovieRepository(MoviesDbContext context)
        {
            _context = context;

            lock (_initLock)
            {
                if (!_inited)
                {
                    InitTestData();
                    _inited = true;
                }
            }
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<int> Count()
        {
            return await _context.Movies.CountAsync();
        }

        public async Task<IEnumerable<Movie>> GetPaginated(int pageNumber, int pageSize)
        {
            return await _context.Movies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Movie?> GetById(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<Movie?> Add(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie?> Update(Movie movie)
        {
            var existing = await _context.Movies.FindAsync(movie.Id);
            if (existing == null) return null;

            existing.Title = movie.Title;
            existing.ReleaseYear = movie.ReleaseYear;
            existing.Genre = movie.Genre;
            existing.ImgUrl = movie.ImgUrl;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<Movie?> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            return movie;
        }

        private void InitTestData()
        {
            if (!_context.Movies.Any())
            {
                _context.Movies.AddRange(
                    new Movie { Id = 101, Title = "Avatar", 
                    ReleaseYear = "2025", Genre = "Action", ImgUrl = "avatar.jpg" },

                    new Movie { Id = 102, Title = "Man In Black", 
                    ReleaseYear = "2016", Genre = "SciFi", ImgUrl = "mib.jpg" },

                    new Movie { Id = 103, Title = "Home ALONE", 
                    ReleaseYear = "1998", Genre = "Comedy", ImgUrl = "hl.jpg" },

                    new Movie { Id = 104, Title = "UP", 
                    ReleaseYear = "2018", Genre = "Drama", ImgUrl = "up.jpg" },

                    new Movie { Id = 105, Title = "The Godfather", 
                    ReleaseYear = "1972", Genre = "Drama", ImgUrl = "godfather.jpg" },

                    new Movie { Id = 106, Title = "Inception", 
                    ReleaseYear = "2010", Genre = "SciFi", ImgUrl = "inception.jpg" },

                    new Movie { Id = 107, Title = "The Dark Knight", 
                    ReleaseYear = "2008", Genre = "Action", ImgUrl = "darkknight.jpg" },

                    new Movie { Id = 108, Title = "Exorcist", 
                    ReleaseYear = "1973", Genre = "Horror", ImgUrl = "exorcist.jpg" },

                    new Movie { Id = 109, Title = "Dumb and Dumber", 
                    ReleaseYear = "1994", Genre = "Comedy", ImgUrl = "dumber.jpg" },

                    new Movie { Id = 110, Title = "Matrix", 
                    ReleaseYear = "1999", Genre = "SciFi", ImgUrl = "matrix.jpg" },

                    new Movie { Id = 111, Title = "Interstellar", 
                    ReleaseYear = "2014", Genre = "SciFi", ImgUrl = "interstellar.jpg" },

                    new Movie { Id = 112, Title = "The Ring", 
                    ReleaseYear = "2002", Genre = "Horror", ImgUrl = "thering.jpg" }
                );
                _context.SaveChanges();
            }
        }
    }
}
