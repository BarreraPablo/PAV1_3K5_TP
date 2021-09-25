using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoIntegradorPav1.Entities;

namespace TrabajoPracticoIntegradorPav1.DataAccesLayer
{
    class NeighborhoodDao
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        public NeighborhoodDao(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public void Add(string nombre)
        {
            SqlCommand cmd = (SqlCommand)connection.CreateCommand();


            string consulta = "INSERT INTO Barrios (nombre) VALUES (@nombre)";

            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@nombre", nombre);

            cmd.CommandText = consulta;
            cmd.Transaction = (SqlTransaction)transaction;

            cmd.ExecuteNonQuery();

        }

        public void Delete(string nombre)
        {
            SqlCommand cmd = (SqlCommand)connection.CreateCommand();


            string consulta = "UPDATE Barrios SET borrado = 1 WHERE nombre LIKE '%'+@nombre+'%'";

            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.CommandText = consulta;
            cmd.Transaction = (SqlTransaction)transaction;

            cmd.ExecuteNonQuery();
        }

        public List<Neighborhood> GetAll()
        {
            SqlCommand cmd = (SqlCommand)connection.CreateCommand();
            var neighborhoods = new List<Neighborhood>();

            string consulta = "SELECT nombre, id_barrio FROM Barrios WHERE borrado is NULL";

            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = consulta;

            DataTable table = new DataTable();
            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                neighborhoods.Add(
                    new Neighborhood()
                    {
                        nombre = dr["nombre"].ToString(),
                        id_barrio = int.Parse(dr["id_barrio"].ToString())
                    });
            }

            return neighborhoods;
        }

        //metodo para obtener un resultado de la base de datos, en este caso como ejemplo obtenemos un barrio
        public List<Neighborhood> GetByName(string nombre)
        {
            var neighborhoods = new List<Neighborhood>();
            SqlCommand cmd = (SqlCommand)connection.CreateCommand();


            string consulta = "SELECT * FROM Barrios WHERE nombre LIKE '%'+@nombre+'%'and borrado is NULL";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@nombre", nombre);

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = consulta;

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                neighborhoods.Add(
                    new Neighborhood()
                    {
                        nombre = dr["nombre"].ToString(),
                        id_barrio = int.Parse(dr["id_barrio"].ToString())
                    });
            }

            return neighborhoods;
        }

        public void Update(string nombre, string id)
        {
            SqlCommand cmd = (SqlCommand)connection.CreateCommand();

            string consulta = "UPDATE Barrios SET nombre = @nombre WHERE id_barrio LIKE @id";

            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.CommandText = consulta;
            cmd.Transaction = (SqlTransaction)transaction;

            cmd.ExecuteNonQuery();

        }
    }
}
