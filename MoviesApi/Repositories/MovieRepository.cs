// TODO: Bo Yang
// TODO: 9086117

using MoviesApi.Data;
using MoviesApi.Models;

namespace MoviesApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesDbContext _context;
        private static bool inited= false;

        public MovieRepository(MoviesDbContext context)
        {
            _context = context;

            if (inited == false)
            {
                initTestData();
                inited = true;
            }
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public int GetTotalCount()
        {
            return _context.Movies.Count();
        }

        public IEnumerable<Movie> GetPaginated(int pageNumber, int pageSize)
        {
            return _context.Movies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Movie? GetById(int id)
        {
            return _context.Movies.Find(id);
        }

        public void Add(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void Update(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
        }

        private void initTestData()
        {
            if (_context.Movies.Count() == 0)
            {
                _context.Add(new Movie { Id = 101, Title = "Avatar", ReleaseYear = "2025", Genre = "Action", ImgUrl = "avatar.jpg" });
                _context.Add(new Movie { Id = 102, Title = "Man In Black", ReleaseYear = "2016", Genre = "SciFi", ImgUrl = "mib.jpg" });
                _context.Add(new Movie { Id = 103, Title = "Home ALONE", ReleaseYear = "1998", Genre = "Comedy", ImgUrl = "hl.jpg" });
                _context.Add(new Movie { Id = 104, Title = "UP", ReleaseYear = "2018", Genre = "Drama", ImgUrl = "up.jpg" });
                _context.Add(new Movie { Id = 105, Title = "The Godfather", ReleaseYear = "1972", Genre = "Drama", ImgUrl = "godfather.jpg" });
                _context.Add(new Movie { Id = 106, Title = "Inception", ReleaseYear = "2010", Genre = "SciFi", ImgUrl = "inception.jpg" });
                _context.Add(new Movie { Id = 107, Title = "The Dark Knight", ReleaseYear = "2008", Genre = "Action", ImgUrl = "darkknight.jpg" });
                _context.Add(new Movie { Id = 108, Title = "Exorcist", ReleaseYear = "1973", Genre = "Horror", ImgUrl = "exorcist.jpg" });
                _context.Add(new Movie { Id = 109, Title = "Dumb and Dumber", ReleaseYear = "1994", Genre = "Comedy", ImgUrl = "dumber.jpg" });
                _context.Add(new Movie { Id = 110, Title = "Matrix", ReleaseYear = "1999", Genre = "SciFi", ImgUrl = "matrix.jpg" });
                _context.Add(new Movie { Id = 111, Title = "Interstellar", ReleaseYear = "2014", Genre = "SciFi", ImgUrl = "interstellar.jpg" });
                _context.Add(new Movie { Id = 112, Title = "The Ring", ReleaseYear = "2002", Genre = "Horror", ImgUrl = "thering.jpg" });
                _context.SaveChanges();
            }

        }
    }
}
