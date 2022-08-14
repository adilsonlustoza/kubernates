using Microsoft.EntityFrameworkCore;
using single_api.Models;

namespace TodoApi.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
            
        }

        public DbSet<Movie> Movies { get; set; } = null!;
    }
}