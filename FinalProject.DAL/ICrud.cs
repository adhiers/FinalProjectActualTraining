using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL
{
    public interface ICrud<T>
    {
        T Create(T item);
        T GetById(string id);
        T Update(T item);
        void Delete(string id);
        IEnumerable<T> GetAll();
    }
}
