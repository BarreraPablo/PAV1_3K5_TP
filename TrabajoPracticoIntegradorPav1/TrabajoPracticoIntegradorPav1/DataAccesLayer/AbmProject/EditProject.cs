using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoIntegradorPav1.Entities;

namespace TrabajoPracticoIntegradorPav1.DataAccesLayer.AbmProject
{
    public class EditProject
    {
        public static bool Edit(Project p)
        {
            bool result = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                string consulta = "UPDATE Proyectos SET id_producto = @id_producto, descripcion = @descripcion, version = @version, alcance = @alcance, id_responsable = @id_responsable WHERE id_proyecto LIKE @id";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", p.id_proyecto);
                cmd.Parameters.AddWithValue("@id_producto", p.id_producto);
                cmd.Parameters.AddWithValue("@descripcion", p.descripcion);
                cmd.Parameters.AddWithValue("@version", p.version);
                cmd.Parameters.AddWithValue("@alcance", p.alcance);
                cmd.Parameters.AddWithValue("@id_responsable", p.id_responsable);
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
