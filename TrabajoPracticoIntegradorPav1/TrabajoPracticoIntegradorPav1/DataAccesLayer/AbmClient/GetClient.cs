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
    public class GetClient
    {
        public static Client Get(string id)
        {
            Client c = new Client();

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                string consulta = "SELECT * FROM Clientes WHERE id_cliente = @id";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null && dr.Read())
                {
                    c.id_cliente = int.Parse(dr["id_cliente"].ToString());
                    c.cuit = long.Parse(dr["cuit"].ToString());
                    c.razon_social = dr["razon_social"].ToString();
                    var borrado = dr["borrado"] as bool?;
                    c.borrado = borrado.HasValue && borrado.Value ? 1 : 0;
                    c.calle = dr["calle"].ToString();
                    c.numero = int.Parse(dr["numero"].ToString());
                    c.fecha_alta = DateTime.Parse(dr["fecha_alta"].ToString());
                    c.id_barrio = int.Parse(dr["id_barrio"].ToString());
                    c.id_contacto = int.Parse(dr["id_contacto"].ToString());
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

            return c;
        }
    }
}
