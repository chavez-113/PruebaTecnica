using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.DataAccess.Repositories
{
    // Interfaz para repositorios que exponen operaciones de CRUD básicas
    interface IRepositorys<T>
    {
        public IEnumerable<T> List();

        public RequestStatus Insert(T item);

        public RequestStatus Update(T item);

        public RequestStatus Delete(int? id);

        public T Find(int? id);

    }
}
