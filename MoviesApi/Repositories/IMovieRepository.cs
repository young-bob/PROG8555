using MoviesApi.Models;

namespace MoviesApi.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<int> Count();
        Task<IEnumerable<Movie>> GetPaginated(int pageNumber, int pageSize);
        Task<Movie?> GetById(int id);
        Task<Movie?> Add(Movie movie);
        Task<Movie?> Update(Movie movie);
        Task<Movie?> Delete(int id);
    }
}
