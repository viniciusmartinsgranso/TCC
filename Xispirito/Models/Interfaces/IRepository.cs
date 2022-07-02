using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xispirito.Models
{
    public interface IRepository<T>
    {
        List<T> List();
        T ReturnId(int id);
        void Insert(T item);
        void Delete(int id);
        void Update(int id, T entity);
    }
}
