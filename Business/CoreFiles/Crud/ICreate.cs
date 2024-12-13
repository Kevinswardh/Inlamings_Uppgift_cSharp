using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Crud
{
    public interface ICreate<T>
    {
        void Create(T entity);
    }
}
