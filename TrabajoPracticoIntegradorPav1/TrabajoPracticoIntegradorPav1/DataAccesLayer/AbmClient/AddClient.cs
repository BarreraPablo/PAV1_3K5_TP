using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoIntegradorPav1.Entities;

namespace TrabajoPracticoIntegradorPav1.DataAccesLayer.AbmClient
{
    public class AddClient
    {
        public static bool Add(Client c)
        {
            bool result = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                string consulta = "INSERT INTO Clientes (cuit, razon_social, calle, numero, fecha_alta, id_barrio) VALUES (@cuit, @razon_social, @calle, @numero, @fecha_alta, @barrio)";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@cuit", c.cuit);
                cmd.Parameters.AddWithValue("@razon_social", c.razon_social);
                cmd.Parameters.AddWithValue("@calle", c.calle);
                cmd.Parameters.AddWithValue("@numero", c.numero);
                cmd.Parameters.AddWithValue("@fecha_alta", c.fecha_alta);
                cmd.Parameters.AddWithValue("@barrio", c.id_barrio);
               

                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }

            return result;
        }
    }
}
