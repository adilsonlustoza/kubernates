using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace single_api.Models
{
    public interface IMemoryLoad<T>
    {
        IEnumerable<T> Movies();
    }
}