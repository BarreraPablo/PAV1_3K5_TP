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
    public class GetProject
    {
        public static Project Get(string id)
        {
            Project p = new Project();

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                string consulta = "SELECT * FROM Proyectos WHERE id_proyecto like @id";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null && dr.Read())
                {
                    p.descripcion = dr["descripcion"].ToString();
                    p.version = dr["version"].ToString();
                    p.alcance = dr["alcance"].ToString();
                    p.id_producto = int.Parse(dr["id_producto"].ToString());
                    p.id_responsable = int.Parse(dr["id_responsable"].ToString());

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

            return p;
        }
    }
}
