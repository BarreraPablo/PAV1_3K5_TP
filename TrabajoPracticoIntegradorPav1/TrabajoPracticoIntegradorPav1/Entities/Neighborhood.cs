using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoIntegradorPav1.Entities
{
    public class Neighborhood
    {

        public int id_barrio { get; set; }
        public string nombre { get; set; }
        public int borrado { get; set; }

        public Neighborhood(int id_barrio, string nombre, int borrado)
        {
            this.id_barrio = id_barrio;
            this.nombre = nombre;
            this.borrado = borrado;
        }
        public Neighborhood()
        {
            
        }
    }
}
