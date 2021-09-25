using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoIntegradorPav1.Entities
{
    public class Client
    {
        public int id_cliente { get; set; }
        public long cuit { get; set; }
        public string razon_social { get; set; }
        public int borrado { get; set; }
        public string calle { get; set; }
        public int numero { get; set; }
        public DateTime fecha_alta { get; set; }
        public int id_barrio { get; set; }
        public int id_contacto { get; set; }

        public Client(int id_cliente, long cuit, string razon_social, int borrado, string calle, int numero, DateTime fecha_alta, int id_barrio, int id_contacto)
        {
            this.id_cliente = id_cliente;
            this.cuit = cuit;
            this.razon_social = razon_social;
            this.borrado = borrado;
            this.calle = calle;
            this.numero = numero;
            this.fecha_alta = fecha_alta;
            this.id_barrio = id_barrio;
            this.id_contacto = id_contacto;
        }
        public Client()
        {

        }

    }
}
