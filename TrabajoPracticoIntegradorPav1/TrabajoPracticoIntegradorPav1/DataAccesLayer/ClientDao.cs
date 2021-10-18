using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoIntegradorPav1.DataAccesLayer
{
    public class ClientDao
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        public ClientDao(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }
    }
}
