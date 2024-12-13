using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Crud
{
    public interface IRead<T>
    {
        T Get(string id);
        List<T> GetAll();
    }
}

