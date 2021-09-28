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
    public class ProductDao : IGeneralDao<Product>
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        public ProductDao(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public void Add(string nombre)
        {
            SqlCommand cmd = (SqlCommand)connection.CreateCommand();


            string consulta = "INSERT INTO Productos (nombre) VALUES (@nombre)";

            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@nombre", nombre);

            cmd.CommandText = consulta;
            cmd.Transaction = (SqlTransaction)transaction;

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlCommand cmd = (SqlCommand)connection.CreateCommand();


            string consulta = "UPDATE Productos SET borrado = 1 WHERE id_producto = @id ";

            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandText = consulta;
            cmd.Transaction = (SqlTransaction)transaction;

            cmd.ExecuteNonQuery();
        }

        public List<Product> GetAll()
        {
            SqlCommand cmd = (SqlCommand)connection.CreateCommand();
            var products = new List<Product>();

            string consulta = "SELECT nombre, id_producto FROM Productos WHERE borrado is NULL or borrado = 0";

            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = consulta;

            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                products.Add(
                    new Product()
                    {
                        nombre = dr["nombre"].ToString(),
                        id = int.Parse(dr["id_producto"].ToString())
                    });
            }

            return products;
        }

        public List<Product> GetByName(string nombre)
        {
            var products = new List<Product>();
            SqlCommand cmd = (SqlCommand)connection.CreateCommand();


            string consulta = "SELECT * FROM Productos WHERE nombre LIKE '%'+@nombre+'%' and (borrado is NULL OR borrado = 0)";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@nombre", nombre);

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = consulta;

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                products.Add(
                    new Product()
                    {
                        nombre = dr["nombre"].ToString(),
                        id = int.Parse(dr["id_producto"].ToString())
                    });
            }

            return products;
        }

        public void Update(string nombre, string id)
        {
            SqlCommand cmd = (SqlCommand)connection.CreateCommand();

            string consulta = "UPDATE Productos SET nombre = @nombre WHERE id_producto = @id";

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
