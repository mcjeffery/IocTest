using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using Movies.Models;

namespace Movies.Services
{
    public class MovieDbService : IMovieDbService, IDisposable
    {
        private readonly MovieDbContext _db = new MovieDbContext();

        public Task<List<Movie>> GetListAsync()
        {
            return _db.Movies.ToListAsync();
        }

        public Task<Movie> GetDetailsAsync(int? id)
        {
            return _db.Movies.FindAsync(id);
        }

        public Task<int> AddMovieAsync(Movie movie)
        {
            _db.Movies.Add(movie);
            return _db.SaveChangesAsync();
        }

        public Task<int> UpdateMovieAsync(Movie movie)
        {
            _db.Entry(movie).State = EntityState.Modified;
            return _db.SaveChangesAsync();
        }

        public async Task<int> DeleteByIdAsync(int? id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie != null)
            {
                _db.Movies.Remove(movie);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}