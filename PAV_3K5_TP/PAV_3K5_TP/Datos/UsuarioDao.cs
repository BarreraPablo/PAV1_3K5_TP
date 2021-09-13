using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV_3K5_TP.Datos
{
    public class UsuarioDao
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public UsuarioDao(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public void MetodoTest()
        {
            string sql = @"INSERT INTO USUARIOS(id_perfil,usuario,password,email,estado,borrado) " +
                "VALUES(1,'TEST','1ASDASD','CONTACTO@GMAIL.COM','S',0)";

            var affectedRows1 = _connection.Execute(sql, transaction: _transaction);
        }
    }
}
