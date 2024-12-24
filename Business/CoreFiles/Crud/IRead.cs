using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Crud
{
    /// <summary>
    /// Ett gränssnitt för att läsa entiteter av en specifik typ.
    /// Tillhandahåller metoder för att hämta en enskild entitet eller alla entiteter.
    /// </summary>
    /// <typeparam name="T">Typen av entitet som ska läsas.</typeparam>
    public interface IRead<T>
    {
        /// <summary>
        /// Hämtar en enskild entitet baserat på dess unika ID.
        /// </summary>
        /// <param name="id">ID för entiteten som ska hämtas.</param>
        /// <returns>Returnerar entiteten med det angivna ID:t.</returns>
        T Get(string id);

        /// <summary>
        /// Hämtar en lista med alla entiteter av typen T.
        /// </summary>
        /// <returns>Returnerar en lista med alla entiteter.</returns>
        List<T> GetAll();
    }
}
