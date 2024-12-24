using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Crud
{
    /// <summary>
    /// Ett gränssnitt för att skapa entiteter av en specifik typ.
    /// Tillhandahåller ett kontrakt för att implementera skapandeoperationen.
    /// </summary>
    /// <typeparam name="T">Typen av entitet som ska skapas.</typeparam>
    public interface ICreate<T>
    {
        /// <summary>
        /// Skapar en ny entitet.
        /// </summary>
        /// <param name="entity">Entiteten som ska skapas.</param>
        void Create(T entity);
    }
}
