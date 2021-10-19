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
    public class ProductService : ISimpleService<Product>
    {
        private UnitOfWork unitOfWork;

        public ProductService()
        {
            unitOfWork = new UnitOfWork();
        }

        public void Add(string nombre)
        {
            var products = GetAll();

            if (products.Any(p => p.nombre.ToLower().Trim() == nombre.ToLower().Trim()))
            {
                throw new EntityAlreadyExistsException();
            }

            unitOfWork.Open();
            unitOfWork.ProductDao.Add(nombre);
            unitOfWork.Close();
        }

        public void Delete(int id)
        {
            unitOfWork.Open();
            unitOfWork.ProductDao.Delete(id);
            unitOfWork.Close();
        }

        public List<Product> GetAll()
        {
            unitOfWork.Open();
            var products = unitOfWork.ProductDao.GetAll();
            unitOfWork.Close();

            return products;
        }

        public List<Product> GetByName(string nombre)
        {
            unitOfWork.Open();
            var products = unitOfWork.ProductDao.GetByName(nombre);
            unitOfWork.Close();

            return products;
        }

        public void Update(string nombre, string id)
        {
            unitOfWork.Open();
            unitOfWork.ProductDao.Update(nombre, id);
            unitOfWork.Close();
        }
    }
}
