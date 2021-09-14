using Dapper;
using PAV_3K5_TP.Negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV_3K5_TP.Datos
{
    public class BarrioDao
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public BarrioDao(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public List<Barrio> obtenerBarrios()
        {
            string sql = "SELECT ID_BARRIO, NOMBRE ";
            sql += "FROM BARRIOS ";
            sql += "WHERE BORRADO=0";


            return _connection.Query<Barrio>(sql).ToList();
        }

        public void guardarBarrio(Barrio barrio)
        {
            string sql = "INSERT INTO BARRIOS(NOMBRE, BORRADO) output INSERTED.ID_BARRIO VALUES (@nombre, 0)";

            barrio.id_barrio = (int)_connection.ExecuteScalar(sql, new { nombre = barrio.nombre }, transaction: _transaction);
        }

        public void borrarBarrio(int id)
        {
            string sql = "UPDATE BARRIOS SET BORRADO=1 WHERE ID_BARRIO=@id_barrio";

            _connection.Execute(sql, new { id_barrio=id } ,transaction: _transaction);
        }

    }
}
