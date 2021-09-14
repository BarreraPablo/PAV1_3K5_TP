using PAV_3K5_TP.Datos;

namespace PAV_3K5_TP.Negocio.Servicios
{
    public class GestorUsuarios
    {
        private UnitOfWork unitOfWork;
        public GestorUsuarios()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool validarUsuario(string userName, string password)
        {
            var usuario = unitOfWork.UsuarioDao.obtenerPorUsernameYPassword(userName, password);

            return usuario != null;
        }
    }
}
