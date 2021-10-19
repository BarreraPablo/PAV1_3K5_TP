using System.Collections.Generic;
using TrabajoPracticoIntegradorPav1.Entities;

namespace TrabajoPracticoIntegradorPav1.Business
{
    public interface ISimpleService<T> 
    {
        void Add(string nombre);
        void Delete(int id);
        List<T> GetAll();
        List<T> GetByName(string nombre);
        void Update(string nombre, string id);
    }
}