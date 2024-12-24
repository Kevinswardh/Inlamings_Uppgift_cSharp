using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Crud
{
    /// <summary>
    /// Ett gränssnitt för att radera entiteter av en specifik typ.
    /// Tillhandahåller ett kontrakt för att implementera radering baserat på ID.
    /// </summary>
    /// <typeparam name="T">Typen av entitet som ska raderas.</typeparam>
    public interface IDelete<T>
    {
        /// <summary>
        /// Raderar en entitet baserat på dess unika ID.
        /// </summary>
        /// <param name="id">ID för entiteten som ska raderas.</param>
        void Delete(string id);
    }
}
