using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoIntegradorPav1.Entities
{
    public class Neighborhood : ISimpleEntity
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public int borrado { get; set; }

        public Neighborhood(int id, string nombre, int borrado)
        {
            this.id = id;
            this.nombre = nombre;
            this.borrado = borrado;
        }
        public Neighborhood()
        {

        }
    }
}
