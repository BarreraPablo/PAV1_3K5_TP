using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoIntegradorPav1.DataAccesLayer
{
    public class UnitOfWork
    {
        private IDbTransaction transaction;
        private IDbConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["conexionDB"]);

        private NeighborhoodDao neighborhoodDao;
        public NeighborhoodDao NeighborhoodDao { 
            get 
            {
                if (neighborhoodDao == null)
                    neighborhoodDao = new NeighborhoodDao(connection, transaction);

                return neighborhoodDao;
            } 
        }

        private ProductDao productDao;

        public ProductDao ProductDao
        {
            get {
                if (productDao == null)
                    productDao = new ProductDao(connection, transaction);
                return productDao; 
            }

        }


        public void Open()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        public void Close()
        {
            if (connection.State != ConnectionState.Open)
                throw new Exception();

            connection.Close();
        }

        public void InitTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            transaction.Commit();
        }

        public void RollbackTransaction()
        {
            transaction.Rollback();
        }

    }
}
