// TODO: Bo Yang
// TODO: 9086117

using MoviesApp.Models;

namespace MoviesApp.Repositories
{
    public interface IRepository
    {
        IEnumerable<Movie> GetAll();
        Movie? GetById(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
    }
}
