using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoIntegradorPav1.Entities
{
    public class Project
    {
       

        public int id_proyecto { get; set; }
        public int id_producto { get; set; }
        public string descripcion  { get; set; }
        public string version { get; set; }
        public string alcance { get; set; }
        public int id_responsable { get; set; }
        public byte borrado { get; set; }

        public Project(int id_proyecto, int id_producto, string descripcion, string version, string alcance, int id_responsable, byte borrado)
        {
            this.id_proyecto = id_proyecto;
            this.id_producto = id_producto;
            this.descripcion = descripcion;
            this.version = version;
            this.alcance = alcance;
            this.id_responsable = id_responsable;
            this.borrado = borrado;
        }

        public Project()
        {

        }
    }
}
