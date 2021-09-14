using PAV_3K5_TP.Datos;
using PAV_3K5_TP.Negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV_3K5_TP.Negocio.Servicios
{
    public class GestorBarrios
    {
        private UnitOfWork unitOfWork;
        public GestorBarrios()
        {
            unitOfWork = new UnitOfWork();
        }

        public List<Barrio> obtenerUsuarios()
        {
            return unitOfWork.BarrioDao.obtenerBarrios();
        } 

        public void agregarBarrio(string nombreBarrio)
        {
            Barrio barrio = new Barrio() { nombre = nombreBarrio };

            unitOfWork.BarrioDao.guardarBarrio(barrio);
        }

        public void borrarBarrio(int id)
        {
            unitOfWork.BarrioDao.borrarBarrio(id);
        }
    }
}
