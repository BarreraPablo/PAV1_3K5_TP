using System;
using System.Data;
using System.Data.SqlClient;

namespace PAV_3K5_TP.Datos
{
    public class UnitOfWork : IDisposable
    {
        public string connString = "Server=localhost;Database=TPS;Trusted_Connection=True;";
        private IDbConnection connection;
        private IDbTransaction transaction;

        public UnitOfWork()
        {
            connection = new SqlConnection(connString);
        }

        private UsuarioDao _usuarioDao;
        public UsuarioDao UsuarioDao { 
            get
            {
                if (_usuarioDao == null) {
                    _usuarioDao = new UsuarioDao(connection, transaction);
                }

                return _usuarioDao;
            }
        }

        private BarrioDao _barrioDao;

        public BarrioDao BarrioDao
        {
            get 
            {
                if (_barrioDao == null)
                    _barrioDao = new BarrioDao(connection, transaction);
                return _barrioDao; 
            }
        }


        public void Open()
        {
            connection.Open();
        }

        public void BeginTransaction()
        {
            if (connection.State != ConnectionState.Open)
                Open();

            transaction = connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            transaction.Commit();
        }

        public void RollBack()
        {
            transaction.Rollback();
        }

        public void Dispose()
        {
            if (transaction != null)
                transaction.Dispose();

            if (connection != null)
                connection.Dispose();
        }
    }
}
