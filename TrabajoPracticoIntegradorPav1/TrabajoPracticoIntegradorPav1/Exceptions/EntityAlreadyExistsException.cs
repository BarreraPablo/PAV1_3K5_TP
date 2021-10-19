using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoIntegradorPav1.Exceptions
{
    class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException() : base("The entity already exists")
        {
        }
    }
}
