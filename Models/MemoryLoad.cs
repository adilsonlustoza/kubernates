using TodoApi.Models;

namespace single_api.Models
{
    public class MemoryLoad : IMemoryLoad<Movie>
    {
   
        private readonly MovieContext _movieContext;
        public MemoryLoad(MovieContext movieContext){
            _movieContext =movieContext;
        }


        public  IEnumerable<Movie> Movies()
        {
          return _movieContext.Movies.AsEnumerable();           
        }



    }
}