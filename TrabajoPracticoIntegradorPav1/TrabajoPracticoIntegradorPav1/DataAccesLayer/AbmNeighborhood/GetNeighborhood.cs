using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoIntegradorPav1.Entities;

namespace TrabajoPracticoIntegradorPav1.DataAccesLayer.AbmNeighborhood
{
    public class GetNeighborhood
    {
        //metodo para obtener un resultado de la base de datos, en este caso como ejemplo obtenemos un barrio
        public static Neighborhood Get(string nombre)
        {
            Neighborhood b = new Neighborhood();

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                string consulta = "SELECT * FROM Barrios WHERE nombre LIKE @nombre";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", nombre);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null && dr.Read())
                {
                    b.nombre = dr["nombre"].ToString();
                    b.id_barrio = int.Parse(dr["id_barrio"].ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }

            return b;
        }
    }
}
