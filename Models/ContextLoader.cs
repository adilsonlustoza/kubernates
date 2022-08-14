using TodoApi.Models;

namespace single_api.Models
{
    public class ContextLoader
    {

      private static readonly string[] Atores = new[]
      {
         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
      };

        private static readonly string[] Nomes = new[]
        {
           "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
         };
     

        public static void Save(MovieContext? context)
        {
            var movies = Enumerable.Range(1, 5).Select(index => new Movie
            {             
                Nome = Nomes[Random.Shared.Next(Nomes.Length)],
                Atores = Atores[Random.Shared.Next(Atores.Length)]
            })
            .ToArray();

            context?.Movies.AddRange(movies);
            context?.SaveChanges();

        }
    }
}