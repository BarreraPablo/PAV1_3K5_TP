using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoIntegradorPav1.DataAccesLayer;
using TrabajoPracticoIntegradorPav1.Entities;
using TrabajoPracticoIntegradorPav1.Exceptions;

namespace TrabajoPracticoIntegradorPav1.Business
{
    public class NeighborhoodService : ISimpleService<Neighborhood>
    {
        private UnitOfWork unitOfWork;

        public NeighborhoodService()
        {
            unitOfWork = new UnitOfWork();
        }

        public void Add(string nombre)
        {
            var neighborhoods = GetAll();

            if(neighborhoods.Any(n => n.nombre.ToLower().Trim() == nombre.ToLower().Trim()))
                throw new EntityAlreadyExistsException();

            unitOfWork.Open();
            unitOfWork.NeighborhoodDao.Add(nombre);
            unitOfWork.Close();
        }

        public void Delete(int id)
        {
            unitOfWork.Open();
            unitOfWork.NeighborhoodDao.Delete(id);
            unitOfWork.Close();
        }
        public List<Neighborhood> GetAll()
        {
            unitOfWork.Open();
            var neightborhoods = unitOfWork.NeighborhoodDao.GetAll();
            unitOfWork.Close();

            return neightborhoods;
        }

        public List<Neighborhood> GetByName(string nombre)
        {
            unitOfWork.Open();
            var neightborhoods = unitOfWork.NeighborhoodDao.GetByName(nombre);
            unitOfWork.Close();

            return neightborhoods;
        }

        public void Update(string nombre, string id)
        {
            unitOfWork.Open();
            unitOfWork.NeighborhoodDao.Update(nombre, id);
            unitOfWork.Close();
        }
    }
}
