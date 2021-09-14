using Dapper;
using PAV_3K5_TP.Negocio.Entidades;
using System.Data;

namespace PAV_3K5_TP.Datos
{
    public class UsuarioDao
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public UsuarioDao(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public Usuario obtenerPorUsernameYPassword(string usuario, string password)
        {
            string sql = @"SELECT ID_PERFIL, USUARIO, PASSWORD, EMAIL, ESTADO, BORRADO ";
            sql += "FROM USUARIOS ";
            sql += "WHERE USUARIO = @usuario AND PASSWORD = @password AND BORRADO = 0";

            return _connection.QueryFirstOrDefault<Usuario>(sql, new { usuario=usuario, password=password } , transaction: _transaction);
        }
    }
}
