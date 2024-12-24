using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Crud
{
    /// <summary>
    /// Ett gränssnitt för att uppdatera entiteter av en specifik typ.
    /// Tillhandahåller en metod för att uppdatera en befintlig entitet.
    /// </summary>
    /// <typeparam name="T">Typen av entitet som ska uppdateras.</typeparam>
    public interface IUpdate<T>
    {
        /// <summary>
        /// Uppdaterar en befintlig entitet i datalagret.
        /// </summary>
        /// <param name="entity">Den entitet som ska uppdateras.</param>
        void Update(T entity);
    }
}
