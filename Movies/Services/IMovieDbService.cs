using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Services
{
    public interface IMovieDbService
    {
        Task<List<Movie>> GetListAsync();
        Task<Movie> GetDetailsAsync(int? id);
        Task<int> AddMovieAsync(Movie movie);
        Task<int> UpdateMovieAsync(Movie movie);
        Task<int> DeleteByIdAsync(int? id);
    }
}