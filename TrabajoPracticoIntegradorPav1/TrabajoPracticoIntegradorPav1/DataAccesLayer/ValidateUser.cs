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
    public class ValidateUser
    {
        public static Usuario UsuarioLogeado = null;
        public static bool Validate(User usu)
        { 
            bool result = false;

            try
            {
                using (var context = new tpDbContext())
                {
                    var usuario = context.Usuarios.FirstOrDefault(u => u.usuario1 == usu.Name && u.password == usu.Password);

                    UsuarioLogeado = usuario;

                    result = usuario != null;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
            }

            return result;
        }
    }
}
