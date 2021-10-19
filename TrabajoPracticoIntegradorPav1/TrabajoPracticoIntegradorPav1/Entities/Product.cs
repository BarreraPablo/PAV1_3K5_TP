using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoIntegradorPav1.Entities
{
    public class Product : ISimpleEntity
    {
        public int id { get; set; }
        public int borrado { get; set; }
        public string nombre { get; set; }
    }
}
