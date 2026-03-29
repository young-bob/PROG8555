// TODO: Bo Yang
// TODO: 9086117

using MoviesApi.Models;

namespace MoviesApi.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        int GetTotalCount();
        IEnumerable<Movie> GetPaginated(int pageNumber, int pageSize);
        Movie? GetById(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
    }
}
