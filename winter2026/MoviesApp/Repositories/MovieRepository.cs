// TODO: Bo Yang
// TODO: 9086117

using MoviesApp.Data;
using MoviesApp.Models;

namespace MoviesApp.Repositories
{
    public class MovieRepository : IRepository
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
                _context.SaveChanges();
            }

        }
    }
}
