using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xispirito.Models
{
    public interface IDatabase<T>
    {
        List<T> List();

        // CRUD - Create.
        void Insert(T entity);

        // CRUD - Read.
        T Select(int entityId);

        // CRUD - Update.
        void Update(T entity);

        // CRUD - Delete.
        void Delete(int entityId);
    }
}